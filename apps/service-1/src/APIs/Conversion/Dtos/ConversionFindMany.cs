using Microsoft.AspNetCore.Mvc;
using Service_1.APIs.Common;
using Service_1.Infrastructure.Models;

namespace Service_1.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ConversionFindMany : FindManyInput<Conversion, ConversionWhereInput> { }
