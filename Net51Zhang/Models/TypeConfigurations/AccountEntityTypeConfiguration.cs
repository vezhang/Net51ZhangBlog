using Net51Zhang.Models.Manager;

namespace Net51Zhang.Models.TypeConfigurations
{
    public class AccountEntityTypeConfiguration : NetEntityTypeConfiguration<AccountEntity>
    {
        public AccountEntityTypeConfiguration()
        {
            this.ToTable("AccountTable");
            HasKey(m => m.Id);
            Property(m => m.CreateTime).IsRequired();
            Property(m => m.Name).IsRequired();
            Property(m => m.HashedPassword).IsRequired();
            Property(m => m.ModifyTime).IsRequired();
        }
    }
}