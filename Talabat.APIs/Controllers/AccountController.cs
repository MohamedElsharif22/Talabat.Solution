﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs.AccountDTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contracts;

namespace Talabat.APIs.Controllers
{
    public class AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IAuthService authService,
        IMapper mapper)
        : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IAuthService _authService = authService;
        private readonly IMapper _mapper = mapper;

        [EndpointSummary("Login as customer")]
        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, "Invalid Email address!"));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, "Invalid Password!"));

            var token = await _authService.CreateTokenAsync(user, _userManager);

            return Ok(user.ToUserResponseAsync(token));

        }

        [EndpointSummary("Register as customer")]
        [HttpPost("Register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest model)
        {

            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.Phone,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors.Select(E => E.Description) });

            var token = await _authService.CreateTokenAsync(user, _userManager);

            return Ok(user.ToUserResponseAsync(token));
        }

        [EndpointSummary("Get Current user")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(email)) return NotFound(new ApiResponse(404));

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) return NotFound(new ApiResponse(404));

            var token = await _authService.CreateTokenAsync(user, _userManager);

            return Ok(user.ToUserResponseAsync(token));

        }

        [EndpointSummary("Get Current user's address")]
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<Address>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressByEmail(User);

            return Ok(_mapper.Map<AddressDTO>(user?.Address));
        }

        [EndpointSummary("Update user's address")]
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<Address>> UpdateUserAddress(AddressDTO address)
        {
            var user = await _userManager.FindUserWithAddressByEmail(User);

            address.Id = user.Address.Id;

            user.Address = _mapper.Map<Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors.Select(E => E.Description) });

            return Ok(_mapper.Map<AddressDTO>(user?.Address));
        }

    }
}
