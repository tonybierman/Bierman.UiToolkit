using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.UiToolkit.ViewModel
{
    public class DetailViewModel : PresentableViewModel
    {
        public DetailViewModel(string formName) : base(formName) { }
        protected override void SaveFormExecute()
        {
            if (Data != null)
            {
                Data.Validate();
                if (Data.IsValid)
                {
                    base.SaveFormExecute();
                }
            }
        }
    }
}
