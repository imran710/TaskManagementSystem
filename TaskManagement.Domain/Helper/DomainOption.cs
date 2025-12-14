using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Helper;
public class DomainOption
{
    public const string SectionName = "Domain";

    public required SecurityOption Security { get; init; }
    public required OtpVerificationOption OtpVerification { get; init; }
    public required AuthOption Auth { get; init; }

    public record SecurityOption
    {
        public const string SectionName = $"{DomainOption.SectionName}:{nameof(Security)}";

        public required string AesSecretKey { get; init; }
    }

    public record OtpVerificationOption
    {
        public const string SectionName = $"{DomainOption.SectionName}:{nameof(OtpVerification)}";

        public required int OtpLength { get; init; }
        public required int OtpValidityDurationInMinutes { get; init; }
        public required bool IsOtpAlphanumeric { get; init; }
        public required int MaxFailAttempt { get; init; }
    }

    public record AuthOption
    {
        public const string SectionName = $"{DomainOption.SectionName}:{nameof(Auth)}";

        public required EmailRegisterFlowType EmailRegisterFlowType { get; init; }
    }
}
