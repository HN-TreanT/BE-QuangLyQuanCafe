﻿using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Material;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;


namespace QuanLyQuanCafe.Services.MaterialServices
{
    public interface IMaterialService
    {
        Task<ApiResponse<Material>> GetMaterialById(string Id);
        Task<ApiResponse<List<Material>>> GetAllMaterial();
        Task<ApiResponse<Material>> CreateMaterial(MaterialDto materialDto);
        Task<ApiResponse<Material>> UpdateMaterial(string Id, MaterialUpdateDto materialUpdateDto);
        Task<ApiResponse<AnyType>> DeleteMaterial(string Id);
        Task<ApiResponse<List<Material>>> searchMaterialByName(string Name);  
    }
}
