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
            SNView snView = new SNView();
            List<SerialNumber> lst =  _serialnumberRepository.GetSerialNumbers();
            if(lst != null && lst.Count > 0)
            {
                snView.lstSN = lst.Select(sn => sn.Serialnumber).ToList();
            }
            return View(snView);
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
            SerialNumber Serialnumber = _serialnumberRepository.GetSerialNumber(id);
            if (Serialnumber != null)
            {
                snView.lstStatus = Serialnumber.BlockChain.Select(sn => sn.Status).ToList();
                snView.SerialNumber = Serialnumber;
            }
            return View(snView);
        }

        [HttpPost]
        [Route("/Home/Status/{id}")]
        public IActionResult Status(SNView snView,string id)
        {
            SerialNumber Serialnumber = _serialnumberRepository.GetSerialNumber(id);
            if (Serialnumber != null)
            {
                string pHash = Serialnumber.BlockChain[Serialnumber.BlockChain.Count - 1].Hash;
               _serialnumberRepository.AddBlock(id, new Block(id, snView.Status, pHash));
            }
            return RedirectToAction("Status",new { id = id});
        }

        public IActionResult Verify()
        {
            return View(new SNView());
        }

        [HttpPost]
        public string Verify(SNView snView)
        {
            string MSG = "";
            string color = "red";
            SerialNumber Serialnumber = _serialnumberRepository.GetSerialNumber(snView.SN);
            if(Serialnumber != null)
            {
                if(Serialnumber.IsValidChain())
                {
                    string strStatus = Serialnumber.BlockChain[Serialnumber.BlockChain.Count - 1].Status;
                    if (strStatus.ToLower() == snView.Status.ToLower())
                    {
                        color = "green";
                        MSG = "Yuppppeee.... Everything is fine.";
                    }
                    else
                    {
                        color = "blue";
                        MSG = "Serial number data unchanged but Current Status is '<b>" + strStatus + "</b>'";
                    }
                }
                else
                {
                    MSG = "<b>Serial number data changed by another person.<b/>";
                }
            }
            else { MSG = "Serial Number '<b>" + snView.SN + "</b>' is not found in database."; }
            return "<div style='color:"+color+";'>"+MSG+"</div>";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
