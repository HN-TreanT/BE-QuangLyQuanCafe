﻿using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.ImportGoods;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.ImportGoodsServices
{
    public interface IImportGoodsService
    {
        Task<ApiResponse<DetailImportGood>> GetDTGoodsById(string Id);
        Task<ApiResponse<List<DetailImportGood>>> GetAllDTGoods();
        Task<ApiResponse<DetailImportGood>> CreateDtIGoods(ImportGoodsDto DtIGoods);
        Task<ApiResponse<DetailImportGood>> UpdateDtIGoods(string Id, ImportGoodsDto DtIGoods);
        Task<ApiResponse<AnyType>> DeleteDtIGoods(string Id);
    }
}