namespace amazon_clone.Models.Models
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; set; }
        public DateOnly? DeleteDate { get; set; }

        public void Delete()
        {
            IsDeleted = true;
            DeleteDate = DateOnly.Parse(DateTime.Now.ToShortDateString());
        }

        public void UndoDelete()
        {
            IsDeleted = false;
            DeleteDate = null;
        }
    }
}
