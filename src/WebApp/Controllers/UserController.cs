using Domain.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetUsers();
        return View(users);
    }

    [HttpGet()]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserDTO user)
    {
        if (ModelState.IsValid)
        {
            await _userService.Add(user);
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    [HttpGet()]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var userDto = await _userService.GetById(id);
        if (userDto == null) return NotFound();
        return View(userDto);
    }

    [HttpPost()]
    public async Task<IActionResult> Edit(UserDTO userDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _userService.Update(userDto);
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(userDto);
    }

    [HttpGet()]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var userDto = await _userService.GetById(id);

        if (userDto == null) return NotFound();

        return View(userDto);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _userService.Remove(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var userDto = await _userService.GetById(id);

        if (userDto == null)
            return NotFound();

        return View(userDto);
    }
}
