namespace Bierman.ViewModel
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
