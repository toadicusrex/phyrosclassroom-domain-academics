namespace PhyrosClassroom.Academics.Models;

public sealed class AcademicAggregate
{
    private readonly List<AcademicEventRecord> _events = [];

    private AcademicAggregate()
    {
    }

    public Guid AcademicId { get; private set; }
    public string AcademicCode { get; private set; } = string.Empty;
    public string SubjectId { get; private set; } = string.Empty;
    public Guid? SourceRegistrationId { get; private set; }
    public string? SourceRegistrationStatus { get; private set; }
    public DateTimeOffset? SourceRegistrationUpdatedAtUtc { get; private set; }
    public string AcademicName { get; private set; } = string.Empty;
    public bool WantsEmailNotifications { get; private set; }
    public bool WantsSmsNotifications { get; private set; }
    public string? OnboardingStatus { get; private set; }
    public IReadOnlyList<AcademicContact> Contacts { get; private set; } = [];
    public IReadOnlyList<AcademicStudent> Students { get; private set; } = [];
    public string GivenName { get; private set; } = string.Empty;
    public string FamilyName { get; private set; } = string.Empty;
    public string? SourceSystem { get; private set; }
    public string? SourceReference { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public string? GradeLevel { get; private set; }
    public string? PrimaryGuardianName { get; private set; }
    public string? PrimaryGuardianEmail { get; private set; }
    public bool HasMedicalAlert { get; private set; }
    public string? MedicalNotes { get; private set; }
    public bool HasIep { get; private set; }
    public IReadOnlyList<AcademicDocumentReference> Documents { get; private set; } = [];
    public IReadOnlyList<AcademicRecordNote> Notes { get; private set; } = [];
    public DateTimeOffset RegisteredAtUtc { get; private set; }
    public IReadOnlyList<AcademicEventRecord> Events => _events;

    public static AcademicAggregate Register(
        Guid academicId,
        string academicCode,
        string givenName,
        string familyName,
        DateTimeOffset occurredUtc,
        string subjectId = "",
        string academicName = "",
        bool wantsEmailNotifications = false,
        bool wantsSmsNotifications = false,
        string? onboardingStatus = null,
        IReadOnlyList<AcademicContact>? contacts = null,
        IReadOnlyList<AcademicStudent>? students = null,
        Guid? sourceRegistrationId = null,
        string? sourceRegistrationStatus = null,
        DateTimeOffset? sourceRegistrationUpdatedAtUtc = null,
        string? sourceSystem = null,
        string? sourceReference = null)
    {
        var aggregate = new AcademicAggregate();
        aggregate.Apply(
            new AcademicEventRecord(
                academicId,
                "AcademicRegistered",
                occurredUtc,
                academicCode,
                givenName.Trim(),
                familyName.Trim(),
                string.IsNullOrWhiteSpace(sourceSystem) ? null : sourceSystem.Trim(),
                string.IsNullOrWhiteSpace(sourceReference) ? null : sourceReference.Trim(),
                null,
                null,
                null,
                null,
                false,
                null,
                false,
                null,
                null,
                string.IsNullOrWhiteSpace(subjectId) ? string.Empty : subjectId.Trim(),
                sourceRegistrationId,
                string.IsNullOrWhiteSpace(sourceRegistrationStatus) ? null : sourceRegistrationStatus.Trim(),
                sourceRegistrationUpdatedAtUtc,
                string.IsNullOrWhiteSpace(academicName) ? string.Empty : academicName.Trim(),
                wantsEmailNotifications,
                wantsSmsNotifications,
                string.IsNullOrWhiteSpace(onboardingStatus) ? null : onboardingStatus.Trim(),
                contacts?.ToArray(),
                students?.ToArray()));

        return aggregate;
    }

    public static AcademicAggregate? Rehydrate(IEnumerable<AcademicEventRecord> eventHistory)
    {
        var aggregate = new AcademicAggregate();

        foreach (var academicEvent in eventHistory.OrderBy(eventItem => eventItem.OccurredUtc))
        {
            aggregate.Apply(academicEvent);
        }

        return aggregate._events.Count == 0 ? null : aggregate;
    }

    public static AcademicAggregate? RehydrateAt(
        IEnumerable<AcademicEventRecord> eventHistory,
        DateTimeOffset pointInTimeUtc)
    {
        return Rehydrate(eventHistory.Where(eventItem => eventItem.OccurredUtc <= pointInTimeUtc));
    }

    public AcademicReadModel ToReadModel()
    {
        return new AcademicReadModel(
            AcademicId,
            AcademicCode,
            GivenName,
            FamilyName,
            RegisteredAtUtc,
            SourceSystem,
            SourceReference,
            BirthDate,
            GradeLevel,
            PrimaryGuardianName,
            PrimaryGuardianEmail,
            HasMedicalAlert,
            MedicalNotes,
            HasIep,
            Documents,
            Notes,
            SubjectId,
            SourceRegistrationId,
            SourceRegistrationStatus,
            SourceRegistrationUpdatedAtUtc,
            AcademicName,
            WantsEmailNotifications,
            WantsSmsNotifications,
            OnboardingStatus,
            Contacts,
            Students,
            _events.LastOrDefault()?.OccurredUtc ?? RegisteredAtUtc);
    }

    public void UpdateProfile(
        string academicName,
        bool wantsEmailNotifications,
        bool wantsSmsNotifications,
        string? onboardingStatus,
        IReadOnlyList<AcademicContact> contacts,
        IReadOnlyList<AcademicStudent> students,
        Guid? sourceRegistrationId,
        string? sourceRegistrationStatus,
        DateTimeOffset? sourceRegistrationUpdatedAtUtc,
        DateOnly? birthDate,
        string? gradeLevel,
        string? primaryGuardianName,
        string? primaryGuardianEmail,
        bool hasMedicalAlert,
        string? medicalNotes,
        bool hasIep,
        IReadOnlyList<AcademicDocumentReference> documents,
        IReadOnlyList<AcademicRecordNote> notes,
        DateTimeOffset occurredUtc)
    {
        Apply(new AcademicEventRecord(
            AcademicId,
            "AcademicProfileUpdated",
            occurredUtc,
            AcademicCode,
            GivenName,
            FamilyName,
            SourceSystem,
            SourceReference,
            birthDate,
            string.IsNullOrWhiteSpace(gradeLevel) ? null : gradeLevel.Trim(),
            string.IsNullOrWhiteSpace(primaryGuardianName) ? null : primaryGuardianName.Trim(),
            string.IsNullOrWhiteSpace(primaryGuardianEmail) ? null : primaryGuardianEmail.Trim(),
            hasMedicalAlert,
            string.IsNullOrWhiteSpace(medicalNotes) ? null : medicalNotes.Trim(),
            hasIep,
            documents.ToArray(),
            notes.ToArray(),
            SubjectId,
            sourceRegistrationId,
            string.IsNullOrWhiteSpace(sourceRegistrationStatus) ? null : sourceRegistrationStatus.Trim(),
            sourceRegistrationUpdatedAtUtc,
            string.IsNullOrWhiteSpace(academicName) ? AcademicName : academicName.Trim(),
            wantsEmailNotifications,
            wantsSmsNotifications,
            string.IsNullOrWhiteSpace(onboardingStatus) ? null : onboardingStatus.Trim(),
            contacts.ToArray(),
            students.ToArray()));
    }

    private void Apply(AcademicEventRecord academicEvent)
    {
        AcademicId = academicEvent.AcademicId;
        AcademicCode = academicEvent.AcademicCode;
        SubjectId = academicEvent.SubjectId;
        SourceRegistrationId = academicEvent.SourceRegistrationId;
        SourceRegistrationStatus = academicEvent.SourceRegistrationStatus;
        SourceRegistrationUpdatedAtUtc = academicEvent.SourceRegistrationUpdatedAtUtc;
        AcademicName = academicEvent.AcademicName;
        WantsEmailNotifications = academicEvent.WantsEmailNotifications;
        WantsSmsNotifications = academicEvent.WantsSmsNotifications;
        OnboardingStatus = academicEvent.OnboardingStatus;
        Contacts = academicEvent.Contacts ?? [];
        Students = academicEvent.Students ?? [];
        GivenName = academicEvent.GivenName;
        FamilyName = academicEvent.FamilyName;
        SourceSystem = academicEvent.SourceSystem;
        SourceReference = academicEvent.SourceReference;
        BirthDate = academicEvent.BirthDate;
        GradeLevel = academicEvent.GradeLevel;
        PrimaryGuardianName = academicEvent.PrimaryGuardianName;
        PrimaryGuardianEmail = academicEvent.PrimaryGuardianEmail;
        HasMedicalAlert = academicEvent.HasMedicalAlert;
        MedicalNotes = academicEvent.MedicalNotes;
        HasIep = academicEvent.HasIep;
        Documents = academicEvent.Documents ?? [];
        Notes = academicEvent.Notes ?? [];

        if (RegisteredAtUtc == default)
        {
            RegisteredAtUtc = academicEvent.OccurredUtc;
        }

        _events.Add(academicEvent);
    }
}
