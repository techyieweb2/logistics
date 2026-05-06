using DriverService.Domain.Enums;
using Shared.Kernel.Common;

namespace DriverService.Domain.Entities
{
    public class Driver : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? Religion { get; set; }
        public string? CivilStatus { get; set; }
        public string? CivilStatusPlace { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? LicenseNumber { get; set; }
        public string? SssNumber { get; set; }
        public string? PhilHealthNumber { get; set; }
        public string? PagIbigNumber { get; set; }
        public string? TinNumber { get; set; }
        public DriverStatus Status { get; set; } = DriverStatus.Active;
        public DateTime? DateHired { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal? FixRate { get; set; }

        // Navigation
        public ICollection<DriverAddress> Addresses { get; set; } = [];
        public ICollection<DriverFamilyMember> FamilyMembers { get; set; } = [];
        public ICollection<DriverEmergencyContact> EmergencyContacts { get; set; } = [];
        public ICollection<DriverEducation> Educations { get; set; } = [];
        public ICollection<DriverEmploymentRecord> EmploymentRecords { get; set; } = [];
        public ICollection<DriverCharacterReference> CharacterReferences { get; set; } = [];
        public ICollection<DriverDocument> Documents { get; set; } = [];
        public ICollection<DriverAssignment> Assignments { get; set; } = [];
        public ICollection<DriverStatusHistory> StatusHistories { get; set; } = [];

        public string FullName => $"{FirstName} {MiddleName} {LastName}".Trim();
    }
}
