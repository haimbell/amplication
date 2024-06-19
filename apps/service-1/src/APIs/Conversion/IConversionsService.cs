using Service_1.APIs.Common;
using Service_1.APIs.Dtos;

namespace Service_1.APIs;

public interface IConversionsService
{
    /// <summary>
    /// Meta data about Conversion records
    /// </summary>
    public Task<MetadataDto> ConversionsMeta(ConversionFindMany findManyArgs);

    /// <summary>
    /// Create one Conversion
    /// </summary>
    public Task<ConversionDto> CreateConversion(ConversionCreateInput conversionDto);

    /// <summary>
    /// Delete one Conversion
    /// </summary>
    public Task DeleteConversion(ConversionIdDto idDto);

    /// <summary>
    /// Find many Conversions
    /// </summary>
    public Task<List<ConversionDto>> Conversions(ConversionFindMany findManyArgs);

    /// <summary>
    /// Get one Conversion
    /// </summary>
    public Task<ConversionDto> Conversion(ConversionIdDto idDto);

    /// <summary>
    /// Update one Conversion
    /// </summary>
    public Task UpdateConversion(ConversionIdDto idDto, ConversionUpdateInput updateDto);
}
