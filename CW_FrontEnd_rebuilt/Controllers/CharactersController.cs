﻿using CW_FrontEnd_rebuilt.ApiManager;
using CW_FrontEnd_rebuilt.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CW_FrontEnd_rebuilt.Controllers
{
    [Route("characters")]
    [ApiController]
    public class CharactersController : Controller
    {
        private IApiController<Character> controller;

        public CharactersController(IApiController<Character> _controller)
        {
            controller = _controller;
        }

        [HttpGet("browse")]
        public IActionResult BrowseCharacters()
        {
            List<Character> model = controller.GetAll();
            return View(model);
        }

        [HttpGet("add")]
        public IActionResult AddCharacter()
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
            Character model = new Character();
            model.userId = int.Parse(HttpContext.Session.GetString("Id"));
            return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("update/{id}")]
        public IActionResult EditCharacter(int id)
        {
            if (int.Parse(HttpContext.Session.GetString("Id")) == id)
            {

                Character model = controller.Get(id);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("supdate")]
        public IActionResult SEditCharacter([FromForm] Character value)
        {
           controller.Update(value);
            return RedirectToAction("BrowseCharacters", "Characters");
        }
        [HttpPost("sadd")]
        public IActionResult SAddCharacter([FromForm] Character value)
        {
            controller.Add(value);
            return RedirectToAction("BrowseCharacters", "Characters");
        }

        [HttpGet("view/{id}")]
        public IActionResult CharacterProfile(int id)
        {
            Character model = controller.Get(id);
            return View(model);
        }
        [HttpGet("remove/{id}")]
        public IActionResult Remove(int id)
        {
            if (int.Parse(HttpContext.Session.GetString("Id")) == id)
            {
                controller.Delete(id);
                return RedirectToAction("Redirection", "Login");
            }
            return RedirectToAction("Index", "Home");
           
        }
    }
}