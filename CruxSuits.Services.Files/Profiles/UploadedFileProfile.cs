using AutoMapper;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.Files;

namespace CruxSuits.Services.Files.Profiles;

public sealed class UploadedFileProfile : Profile
{
    public UploadedFileProfile()
    {
        CreateMap<UploadFileRequest, UploadedFile>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.OriginalFileName,
                options => options.MapFrom(source => NormalizeFileName(source.File.FileName)))
            .ForMember(destination => destination.SystemFileName, options => options.Ignore())
            .ForMember(destination => destination.Extension, options => options.Ignore())
            .ForMember(destination => destination.FileType, options => options.Ignore())
            .ForMember(destination => destination.SizeInBytes,
                options => options.MapFrom(source => source.File.Length))
            .ForMember(destination => destination.AbsolutePath, options => options.Ignore())
            .ForMember(destination => destination.Content, options => options.Ignore());

        CreateMap<UpdateFileRequest, UploadedFile>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.OriginalFileName, options => options.Ignore())
            .ForMember(destination => destination.SystemFileName, options => options.Ignore())
            .ForMember(destination => destination.Extension, options => options.Ignore())
            .ForMember(destination => destination.FileType, options => options.Ignore())
            .ForMember(destination => destination.SizeInBytes, options => options.Ignore())
            .ForMember(destination => destination.AbsolutePath, options => options.Ignore())
            .ForMember(destination => destination.Content, options => options.Ignore());

        CreateMap<UploadedFile, UploadFileResponse>();
    }

    private static string NormalizeFileName(string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return "file";

        var normalizedName = Path.GetFileName(fileName.Replace('\\', '/'));
        return string.IsNullOrWhiteSpace(normalizedName) ? "file" : normalizedName;
    }
}
