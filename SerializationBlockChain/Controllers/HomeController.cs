using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerializationBlockChain.DBLayer.Repository;
using SerializationBlockChain.Models;

namespace SerializationBlockChain.Controllers
{
    public class HomeController : Controller
    {
        ISerialnumberRepository _serialnumberRepository;

        public HomeController(ISerialnumberRepository serialnumberRepository)
        {
            _serialnumberRepository = serialnumberRepository;
        }

        public IActionResult Index()
        {
            return View(new SNView());
        }

        [HttpPost]
        public IActionResult Index(SNView snView)
        {
            _serialnumberRepository.CreateSerialNumber(new SerialNumber(snView.SN));
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/Home/Status/{id}")]
        public IActionResult Status(string id)
        {
            SNView snView = new SNView();
            snView.SN = id;
            return View(snView);
        }

        [HttpPost]
        public IActionResult Status(SNView snView)
        {
            return View();
        }

        public IActionResult Verify()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSerialnumber(SNView snView)
        {
            return View(snView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
