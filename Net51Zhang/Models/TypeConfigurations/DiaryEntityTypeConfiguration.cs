using Net51Zhang.Models.Diary.EntityModel;

namespace Net51Zhang.Models.TypeConfigurations
{
    public class DiaryEntityTypeConfiguration : NetEntityTypeConfiguration<DiaryEntity>
    {
        public DiaryEntityTypeConfiguration()
        {
            this.ToTable("DiaryTable");
            HasKey(m => m.Id);
            Property(m => m.CreateTime).IsRequired();
            Property(m => m.Content).IsRequired();
            Property(m => m.Title).IsRequired();
            Property(m => m.Tag).IsRequired();
        }
    }
}