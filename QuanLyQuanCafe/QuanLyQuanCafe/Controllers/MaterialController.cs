﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Material;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.MaterialServices;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly IMaterialService _materialService;
        public MaterialController(IMapper mapper, CafeContext context, IMaterialService materialService)
        {
            _mapper = mapper;
            _context = context;
            _materialService = materialService;
        }
        [HttpGet]
        [Route("getMaterialById/{Id}")]

        public async Task<IActionResult> GetMaterialById(string Id)
        {
            try
            {
                var response = await _materialService.GetMaterialById(Id);  
                return Ok(response);
            }catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllMaterial")]

        public async Task<IActionResult> GetAllMaterial()
        {
            try
            {
                var response = await _materialService.GetAllMaterial();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("createMaterial")]

        public async Task<IActionResult> CreateMaterial([FromBody]MaterialDto materialDto)
        {
            try
            {
                var response = await _materialService.CreateMaterial(materialDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateMaterial/{Id}")]

        public async Task<IActionResult> UpdateMaterial(string Id, [FromBody] MaterialUpdateDto materialDto)
        {
            try
            {
                var response = await _materialService.UpdateMaterial(Id,materialDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteMaterial/{Id}")]

        public async Task<IActionResult> DelteMaterial(string Id)
        {
            try
            {
                var response = await _materialService.DeleteMaterial(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("searchMaterial/{name}")]

        public async Task<IActionResult> SearchMaterial(string name)
        {
            try
            {
                var response = await _materialService.searchMaterialByName(name);
                if(response.Data.Count <= 0)
                {
                    return Ok(new ApiResponse<AnyType> { Status = false , Message = "Not found"});
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
