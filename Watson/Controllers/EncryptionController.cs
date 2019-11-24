using System;
using System.Web.Mvc;
using Watson.Models;
using System.Security.Cryptography;
using System.IO;

namespace Watson.Controllers
{
    public class EncryptionController : System.Web.Mvc.Controller
    {

        private static Encryption encrypt = new Encryption();

        public EncryptionController()
        {

        }

        public EncryptionController(string empSSN, string empDOB, string spSSN, string spDOB, string depSSN, string depDOB, string otherPolicyNum, string otherInsCarrier)
        {
   
        }
       
        // GET: Encryption
        public ActionResult MasterEncryption(string encrypt)
        {

            return View(encrypt);
        }

    }
}