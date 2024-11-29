using AutoMapper;
using FastFoodManagement.Data.DTO.Branch;
using FastFoodManagement.Model.Models;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagement.Web.Controllers;

[Route("api/branch")]
[ApiController]
public class BranchController : ControllerBase
{
    private readonly IBranchService _branchService;
    private readonly IMapper _mapper;
    public BranchController(IBranchService branchService, IMapper mapper)
    {
        _branchService = branchService;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<ActionResult<ApiResponse<List<RetrieveBranchDTO>>>> GetAllBranches()
    {
        try
        {
            var branches = await _branchService.GetAllBranches();
            var branchDTOs = _mapper.Map<List<RetrieveBranchDTO>>(branches);
            var response = ApiResponse<List<RetrieveBranchDTO>>.SuccessResponse(branchDTOs, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<RetrieveBranchDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<RetrieveBranchDTO>>> GetBranchById(int id)
    {
        try
        {
            var branch = await _branchService.GetBranchById(id);
            var branchDTO = _mapper.Map<RetrieveBranchDTO>(branch);
            var response = ApiResponse<RetrieveBranchDTO>.SuccessResponse(branchDTO, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<RetrieveBranchDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<CreateBranchDTO>>> CreateBranch(CreateBranchDTO branchDTO)
    {
        try
        {
            var branch = _mapper.Map<Branch>(branchDTO);

            // Created branch is active by default
            branch.IsActive = true;
            
            await _branchService.AddBranch(branch);
            var response = ApiResponse<CreateBranchDTO>.SuccessResponse(branchDTO, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<CreateBranchDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteBranchById(int id)
    {
        try
        {
            await _branchService.DeleteBranchById(id);
            var response = new ApiResponse<RetrieveBranchDTO>(message: $"Branch ${id} deleted successfully", code: 200, success: true);
            return Ok(response);
        }
        catch (Exception e)
        {
            var response = ApiResponse<RetrieveBranchDTO>.ErrorResponse(e.Message, new List<string> { e.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpPatch("update/{id:int}")]
    public async Task<ActionResult<ApiResponse<UpdateBranchDTO>>> UpdateBranch(int id, UpdateBranchDTO branchDTO)
    {
        try
        {
            var branch = await _branchService.GetBranchById(id);
            if (branch == null)
            {
                var response = ApiResponse<UpdateBranchDTO>.ErrorResponse("Branch not found", new List<string> { "Branch not found" }, 404);
                return NotFound(response);
            }
            
            // Only update the properties that are not null
            if (branchDTO.Name != null)
            {
                branch.Name = branchDTO.Name;
            }
            if (branchDTO.Location != null)
            {
                branch.Location = branchDTO.Location;
            }
            if (branchDTO.Phone != null)
            {
                branch.Phone = branchDTO.Phone;
            }
            if (branchDTO.Email != null)
            {
                branch.Email = branchDTO.Email;
            }
            if (branchDTO.IsActive != null)
            {
                branch.IsActive = branchDTO.IsActive.Value;
            }
            
            // Update the branch in the database
            await _branchService.UpdateBranch(branch);
            var branchDTOResponse = _mapper.Map<UpdateBranchDTO>(branch);
            var response1 = ApiResponse<UpdateBranchDTO>.SuccessResponse(branchDTOResponse, code: 200);
            return Ok(response1);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<UpdateBranchDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
}