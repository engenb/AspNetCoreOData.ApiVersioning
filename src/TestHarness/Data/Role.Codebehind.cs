using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace TestHarness.Data;

public partial class Role
{
    private static readonly MD5 MD5 = MD5.Create();
    
    public static implicit operator string(Role r) =>r.Name;
    public static implicit operator Role(string s)
    {
        try
        {
            return FromName(s);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new InvalidCastException($"{s} is not a valid Role");
        }
    }

    public static Role FromName(string name)
    {
        if (!AllConstants.Contains(name)) throw new ArgumentOutOfRangeException(nameof(name));
        return new Role {Id = ComputeId(name), Name = name};
    }

    public static Guid ComputeId(string name) =>
        new (MD5.ComputeHash(Encoding.Default.GetBytes(name)));

    private static readonly Lazy<ISet<string>> LazyAllConstants = new(() =>
        new HashSet<string>(typeof(Role)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(x => x.FieldType == typeof(string))
            .Select(x => (string) x.GetValue(null))));

    public static ISet<string> AllConstants => LazyAllConstants.Value;
    
    private static readonly Lazy<IDictionary<Guid, Role>> LazyRoles = new(() => AllConstants
        .Select(FromName)
        .ToDictionary(x => x.Id));

    public static IDictionary<Guid, Role> All => LazyRoles.Value;

    public const string AccountRead = "Account.Read";
    public const string AccountReadWrite = "Account.ReadWrite";
    public const string AccountReadWriteAll = "Account.ReadWrite.All";
    public const string AssetTypeRead = "AssetType.Read";
    public const string AssetTypeReadWrite = "AssetType.ReadWrite";
    public const string AssetTypeReadWriteAll = "AssetType.ReadWrite.All";
    public const string AuditRead = "Audit.Read";
    public const string AuditReadAll = "Audit.Read.All";
    public const string ClientRead = "Client.Read";
    public const string ClientReadWrite = "Client.ReadWrite";
    public const string ClientReadWriteAll = "Client.ReadWrite.All";
    public const string CostCenterRead = "CostCenter.Read";
    public const string CostCenterReadWrite = "CostCenter.ReadWrite";
    public const string CostCenterReadWriteAll = "CostCenter.ReadWrite.All";
    public const string CustomFieldRead = "CustomField.Read";
    public const string CustomFieldReadWrite = "CustomField.ReadWrite";
    public const string CustomFieldReadWriteAll = "CustomField.ReadWrite.All";
    public const string ExchangeRateRead = "ExchangeRate.Read";
    public const string ExchangeRateReadWrite = "ExchangeRate.ReadWrite";
    public const string ExchangeRateReadWriteAll = "ExchangeRate.ReadWrite.All";
    public const string FirmRead = "Firm.Read";
    public const string FirmReadWrite = "Firm.ReadWrite";
    public const string FirmReadWriteAll = "Firm.ReadWrite.All";
    public const string LeaseRead = "Lease.Read";
    public const string LeaseReadWrite = "Lease.ReadWrite";
    public const string LeaseReadWriteAll = "Lease.ReadWrite.All";
    public const string LessorRead = "Lessor.Read";
    public const string LessorReadWrite = "Lessor.ReadWrite";
    public const string LessorReadWriteAll = "Lessor.ReadWrite.All";
    public const string PolicyRead = "Policy.Read";
    public const string PolicyReadWrite = "Policy.ReadWrite";
    public const string PolicyReadWriteAll = "Policy.ReadWrite.All";
    public const string ReportingEntityRead = "ReportingEntity.Read";
    public const string ReportingEntityReadWrite = "ReportingEntity.ReadWrite";
    public const string ReportingEntityReadWriteAll = "ReportingEntity.ReadWrite.All";
    public const string UserRead = "User.Read";
    public const string UserReadWrite = "User.ReadWrite";
    public const string UserReadWriteAll = "User.ReadWrite.All";
    public const string UserReadWriteClient = "User.ReadWrite.Client";
}