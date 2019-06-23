using System;
namespace CuponeraUniversitaria.Infrastructure
{
    public class PaginationOptions
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PaginationOptions WithPageSize(int pageSize)
        {
            this.PageSize = pageSize;
            return this;
        }

        private PaginationOptions(int page)
        {
            this.Page = page;
            this.PageSize = 10;
        }

        public static PaginationOptions Create()
        {
            return new PaginationOptions(1);
        }

        public static PaginationOptions Create(int page)
        {
            return new PaginationOptions(page);
        }
    }
}
