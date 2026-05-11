using System.ComponentModel.DataAnnotations;

namespace SponsorshipApproval.Api.Application.DTOs;

public sealed record LoginRequestDto(
    [property: Required, EmailAddress, MaxLength(256)] string Email,
    [property: Required, MinLength(8), MaxLength(128)] string Password);

public sealed record AuthUserDto(int Id, string FullName, string Email, string Department, string Role);

public sealed record LoginResponseDto(string AccessToken, DateTime ExpiresAtUtc, AuthUserDto User);
