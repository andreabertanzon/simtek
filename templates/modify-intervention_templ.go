// Code generated by templ - DO NOT EDIT.

// templ: version: v0.2.543
package templates

//lint:file-ignore SA4006 This context is only used if a nested component is present.

import "github.com/a-h/templ"
import "context"
import "io"
import "bytes"

import "github.com/andreabertanzon/simtek/models"

func interventionPut(intervention models.Intervention) string {
	return "/intervention/" + intervention.Timestamp
}

func ModifyIntervention(intervention models.Intervention) templ.Component {
	return templ.ComponentFunc(func(ctx context.Context, templ_7745c5c3_W io.Writer) (templ_7745c5c3_Err error) {
		templ_7745c5c3_Buffer, templ_7745c5c3_IsBuffer := templ_7745c5c3_W.(*bytes.Buffer)
		if !templ_7745c5c3_IsBuffer {
			templ_7745c5c3_Buffer = templ.GetBuffer()
			defer templ.ReleaseBuffer(templ_7745c5c3_Buffer)
		}
		ctx = templ.InitializeContext(ctx)
		templ_7745c5c3_Var1 := templ.GetChildren(ctx)
		if templ_7745c5c3_Var1 == nil {
			templ_7745c5c3_Var1 = templ.NopComponent
		}
		ctx = templ.ClearChildren(ctx)
		_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString("<form class=\"flex flex-col htmx-swapping:opacity-0 transition-opacity duration-700\" id=\"add-todo-form\" hx-put=\"")
		if templ_7745c5c3_Err != nil {
			return templ_7745c5c3_Err
		}
		_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString(templ.EscapeString(interventionPut(intervention)))
		if templ_7745c5c3_Err != nil {
			return templ_7745c5c3_Err
		}
		_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString("\" hx-target=\"#intervention-content\" hx-swap=\"innerHTML\"><div class=\"flex flex-col\"><label for=\"site\" class=\"text-4xl\">Cantiere</label> <input class=\"mt-2 p-2 border text-4xl\" type=\"text\" name=\"site\" required value=\"")
		if templ_7745c5c3_Err != nil {
			return templ_7745c5c3_Err
		}
		_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString(templ.EscapeString(intervention.ToViewModel().Site))
		if templ_7745c5c3_Err != nil {
			return templ_7745c5c3_Err
		}
		_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString("\"> <label for=\"intervention\" class=\"text-4xl mt-2\">Intervento</label> <textarea rows=\"6\" class=\"mt-2 text-4xl p-2 border min-h-52\" type=\"text\" name=\"intervention\" required>")
		if templ_7745c5c3_Err != nil {
			return templ_7745c5c3_Err
		}
		var templ_7745c5c3_Var2 string
		templ_7745c5c3_Var2, templ_7745c5c3_Err = templ.JoinStringErrs(intervention.ToViewModel().Intervention)
		if templ_7745c5c3_Err != nil {
			return templ.Error{Err: templ_7745c5c3_Err, FileName: `templates/modify-intervention.templ`, Line: 33, Col: 45}
		}
		_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString(templ.EscapeString(templ_7745c5c3_Var2))
		if templ_7745c5c3_Err != nil {
			return templ_7745c5c3_Err
		}
		_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString("</textarea></div><div class=\"flex flex-col mt-4\" id=\"workers-container\"><p class=\"text-4xl\">Operatori</p>")
		if templ_7745c5c3_Err != nil {
			return templ_7745c5c3_Err
		}
		for _, worker := range intervention.ToViewModel().Workers {
			_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString("<input class=\"mt-2 p-2 border text-4xl\" type=\"text\" name=\"workers[]\" required value=\"")
			if templ_7745c5c3_Err != nil {
				return templ_7745c5c3_Err
			}
			_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString(templ.EscapeString(worker))
			if templ_7745c5c3_Err != nil {
				return templ_7745c5c3_Err
			}
			_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString("\">")
			if templ_7745c5c3_Err != nil {
				return templ_7745c5c3_Err
			}
		}
		_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString("</div><button class=\"btn-secondary rounded p-2 mt-2 mr-2 text-4xl\" type=\"button\" hx-get=\"/dynamic-input?type=worker\" hx-target=\"#workers-container\" hx-swap=\"beforeend\">+Operatore</button><div class=\"flex flex-col mt-4\" id=\"materials-container\"><p class=\"text-4xl\">Materiali</p>")
		if templ_7745c5c3_Err != nil {
			return templ_7745c5c3_Err
		}
		for _, material := range intervention.ToViewModel().Materials {
			_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString("<input class=\"mt-2 p-2 border text-4xl\" type=\"text\" name=\"materials[]\" required value=\"")
			if templ_7745c5c3_Err != nil {
				return templ_7745c5c3_Err
			}
			_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString(templ.EscapeString(material))
			if templ_7745c5c3_Err != nil {
				return templ_7745c5c3_Err
			}
			_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString("\">")
			if templ_7745c5c3_Err != nil {
				return templ_7745c5c3_Err
			}
		}
		_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteString("</div><div class=\"flex\"><button class=\"btn-secondary rounded p-2 mt-2 mr-2 text-4xl\" type=\"button\" hx-get=\"/dynamic-input?type=material\" hx-target=\"#materials-container\" hx-swap=\"beforeend\">+Materiale</button> <button class=\"btn-primary rounded p-2 mr-2 mt-2 text-4xl\" type=\"submit\">Avanti</button> <button class=\"rounded border-2 p-2 mt-2 text-4xl btn-outline-danger\" hx-get=\"/\" hx-target=\"#main-content\" hx-boost=\"true\">Annulla</button></div></form>")
		if templ_7745c5c3_Err != nil {
			return templ_7745c5c3_Err
		}
		if !templ_7745c5c3_IsBuffer {
			_, templ_7745c5c3_Err = templ_7745c5c3_Buffer.WriteTo(templ_7745c5c3_W)
		}
		return templ_7745c5c3_Err
	})
}
