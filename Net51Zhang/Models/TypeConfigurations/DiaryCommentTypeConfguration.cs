using Net51Zhang.Models.Diary.EntityModel;

namespace Net51Zhang.Models.TypeConfigurations
{
    public class DiaryCommentTypeConfguration : NetEntityTypeConfiguration<DiaryCommentEntity>
    {
        public DiaryCommentTypeConfguration()
        {
            this.ToTable("DiaryCommentTable");
            HasKey(m => m.Id);
            Property(m => m.CreatedTime).IsRequired();
            Property(m => m.Content).IsRequired();
            Property(m => m.DiaryId).IsRequired();
            Property(m => m.Email).IsRequired();
            Property(m => m.NickName).IsRequired();
        }
    }
}