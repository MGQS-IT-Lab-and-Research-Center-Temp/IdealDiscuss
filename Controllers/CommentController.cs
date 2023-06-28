using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdealDiscuss.Models.Comment;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace IdealDiscuss.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    private readonly INotyfService _notyf;

    public CommentController(ICommentService commentService, INotyfService notyf)
    {
        _commentService = commentService;
        _notyf = notyf; 
    }

    //public IActionResult Index()
    //{
    //    var response = _commentService.GetAllComment();
    //    ViewData["Message"] = response.Message;
    //    ViewData["Status"] = response.Status;

    //    return View(response.Data);
    //}

    public async Task<IActionResult> GetCommentDetail(string id)
    {
        var response = await _commentService.GetComment(id);

        if (response.Status is false)
        {
            _notyf.Error(response.Message);
            return RedirectToAction("Index", "Question");
        }

        return View(response.Data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCommentViewModel request)
    {
        var response = await _commentService.CreateComment(request);

        if(response.Status is false)
        {
            _notyf.Error(response.Message);
            return View();
        }

        _notyf.Success(response.Message);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Update(string id)
    {
        var response = await _commentService.GetComment(id);

        if (response.Status is false)
        {
            _notyf.Error(response.Message);
            return RedirectToAction("Index", "Home");
        }

        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, UpdateCommentViewModel request)
    {
        var response = await _commentService.UpdateComment(id, request);

        if (response.Status is false)
        {
            _notyf.Error(response.Message);
            return RedirectToAction("Index", "Home");
        }

        _notyf.Success(response.Message);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteComment([FromRoute] string id)
    {
        var response = await _commentService.DeleteComment(id);

        if (response.Status is false)
        {
            _notyf.Error(response.Message);
            return RedirectToAction("Index", "Home");
        }

        _notyf.Success(response.Message);

        return RedirectToAction("Index", "Home");
    }
}
