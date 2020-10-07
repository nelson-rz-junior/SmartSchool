namespace SmartSchool.API.Helpers
{
    public class PageParameters
    {
        public const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize 
        { 
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }

        public int? Matricula { get; set; } = null;

        public string Nome { get; set; } = string.Empty;

        public bool? Status { get; set; } = null;
    }
}