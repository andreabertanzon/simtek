using System.Drawing;
using MediatR;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Simtek.Application.Commands;
using Simtek.Domain.Helpers;
using Simtek.Application.Repositories;
using Simtek.Domain;

namespace Simtek.Application.Handlers;

public class GenerateExcelCommandHandler(IInterventionRepository interventionRepository)
    : IRequestHandler<GenerateExcelCommand, OperationResult<Stream>>
{
    public async Task<OperationResult<Stream>> Handle(GenerateExcelCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var intervention =
                await interventionRepository.GetInterventionAsync(request.InterventionId, cancellationToken);
            var currentIncrease = 0;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage("Template/Intervento_template.xlsx");

            var worksheet = package.Workbook.Worksheets[0];

            // A4 - Site
            worksheet.Cells["A5"].Value = intervention.Site.Name;
            worksheet.Cells["A6"].Value = intervention.Site.Address;
            worksheet.Cells["A7"].Value = intervention.Site.CustomerName;

            // C4 - Date
            worksheet.Cells["C4"].Value = intervention.Date.ToString("dd/MM/yyyy");

            // A10 - Operators
            // euro symbol is: €
            // operators start from A11. Though the first operator is A11, all the 
            // other operators will be placed in the next row that needs to be added. So add
            // as many rows as there are operators. Then start filling those new rows with the operators.

            var operators = intervention.Operators;
            var operatorsRow = 11;
            // count the number of operators and add that many rows
            worksheet.InsertRow(operatorsRow, operators.Count);
            foreach (var (worker, hoursWorked) in operators)
            {
                worksheet.Cells[$"A{operatorsRow}:D{operatorsRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[$"A{operatorsRow}:D{operatorsRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[$"A{operatorsRow}"].Value = worker.Name + " " + worker.Surname;
                worksheet.Cells[$"B{operatorsRow}"].Value = hoursWorked;
                worksheet.Cells[$"B{operatorsRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[$"C{operatorsRow}"].Value = "€" + worker.PricePerHour;
                worksheet.Cells[$"C{operatorsRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[$"D{operatorsRow}"].Value = "€" + (worker.PricePerHour * hoursWorked).ToString("0.00");
                worksheet.Cells[$"D{operatorsRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                operatorsRow++;
            }

            // A14+currentIncrease - Description
            var descriptionRow = operatorsRow + 3;
            var descriptions = intervention.Description.Split("\n");
            worksheet.InsertRow(descriptionRow, descriptions.Length);
            foreach (var description in descriptions)
            {
                worksheet.Cells[$"A{descriptionRow}:D{descriptionRow}"].Merge = true;
                worksheet.Cells[$"A{descriptionRow}:D{descriptionRow}"].Value = description;
                worksheet.Cells[$"A{descriptionRow}:D{descriptionRow}"].Style.WrapText = true;
                worksheet.Cells[$"A{descriptionRow}:D{descriptionRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[$"A{descriptionRow}:D{descriptionRow}"].Style.Border.Right.Style =
                    ExcelBorderStyle.Thin;
                descriptionRow++;
            }

            // Materials
            var materialsRow = descriptionRow + 5;
            var materials = intervention.Materials ?? new List<Material>();
            if (materials.Count > 0)
            {
                worksheet.InsertRow(materialsRow, materials.Count);

                foreach (var material in materials)
                {
                    worksheet.Cells[$"A{materialsRow}:D{materialsRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A{materialsRow}:D{materialsRow}"].Style.Border.Right.Style =
                        ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A{materialsRow}"].Value = material.UnitMeasure;
                    worksheet.Cells[$"A{materialsRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[$"B{materialsRow}"].Value = material.Quantity;
                    worksheet.Cells[$"B{materialsRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[$"C{materialsRow}"].Value = material.Name;
                    worksheet.Cells[$"C{materialsRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[$"D{materialsRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    materialsRow++;
                }
            }

            // Notes
            var notesRow = materialsRow + 4;
            worksheet.Cells[$"A{notesRow}"].Value = intervention.Notes;

            var stream = new MemoryStream(await package.GetAsByteArrayAsync(cancellationToken));
            return OperationResult<Stream>.Success(stream);
        }
        catch (Exception e)
        {
            return OperationResult<Stream>.Failure(e);
        }
    }
}