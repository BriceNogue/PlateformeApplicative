
// Pour personnaliser l'affichage des items && pages dans un flyout

namespace Mobile.ViewModels
{
    public class FlyoutPageItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
    }
}
