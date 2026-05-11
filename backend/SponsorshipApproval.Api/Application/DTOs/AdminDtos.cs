using System.ComponentModel.DataAnnotations;
using SponsorshipApproval.Api.Domain.Enums;

namespace SponsorshipApproval.Api.Application.DTOs;

public sealed record SponsorshipTypeDto(int Id, string Name, string? Description, bool IsActive);

public sealed record SponsorshipTypeCreateDto(
    [property: Required, MaxLength(120)] string Name,
    [property: MaxLength(500)] string? Description,
    bool IsActive = true);

public sealed record SponsorshipTypeUpdateDto(
    [property: Required, MaxLength(120)] string Name,
    [property: MaxLength(500)] string? Description,
    bool IsActive);

public sealed record ApprovalHistoryDto(
    int Id,
    WorkflowStatus? FromStatus,
    WorkflowStatus ToStatus,
    WorkflowAction Action,
    string? Remarks,
    string ActionBy,
    string ActionByRole,
    DateTime ActionAt);
