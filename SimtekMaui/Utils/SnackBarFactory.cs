using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace SimtekMaui.Utils
{
    public static class SnackbarFactory
    {
        public static ISnackbar MakeSnackBar(SnackbarType snackBarType, string text, string actionButtonText, TimeSpan duration)
        {
            SnackbarOptions snackbarOptions;
            ISnackbar snackbar;

            switch (snackBarType)
            {
                case SnackbarType.Error:
                    snackbarOptions = new SnackbarOptions
                    {
                        BackgroundColor = Color.FromArgb("F2B8B5"),
                        TextColor = Color.FromArgb("601410"),
                        ActionButtonTextColor = Color.FromArgb("601410"),
                        CornerRadius = new CornerRadius(10),
                    };
                    snackbar = Snackbar.Make(text, null, actionButtonText, duration, snackbarOptions);
                    break;

                case SnackbarType.Success:
                case SnackbarType.Warning:
                    snackbarOptions = new SnackbarOptions
                    {
                        BackgroundColor = Color.FromArgb("F2B8B5"),
                        TextColor = Color.FromArgb("601410"),
                        ActionButtonTextColor = Color.FromArgb("601410"),
                        CornerRadius = new CornerRadius(10),
                    };


                    snackbar = Snackbar.Make(text, null, actionButtonText, duration, snackbarOptions);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(snackBarType), snackBarType, null);
            }

            return snackbar;
        }
    }

    public enum SnackbarType
    {
        Error,
        Success,
        Warning
    }
}
