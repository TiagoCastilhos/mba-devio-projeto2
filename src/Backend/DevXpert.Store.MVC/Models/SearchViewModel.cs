using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevXpert.Store.MVC.Models
{
    public class SearchViewModel
    {
        public string ControllerToRedirect { get; set; }
        public List<SelectListItem> Status{ get; set; }

        public SearchViewModel()
        {

        }

        public SearchViewModel(string controllerToRedirect, List<SelectListItem> status)
        {
            ControllerToRedirect = controllerToRedirect;
            Status = status;
        }
    }
}
