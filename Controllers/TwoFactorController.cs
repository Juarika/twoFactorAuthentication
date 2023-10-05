using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using twoFactorAuthentication.Entities;
using TwoFactorAuthNet;
using TwoFactorAuthNet.Providers.Qr;

namespace twoFactorAuthentication.Controllers;

public class TwoFactorController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet, Route("GetQRCode")]
    public string GetQRCode(string email)
    {
        var tfa = new TwoFactorAuth("ManuelToscanoDEV", 6, 30, Algorithm.SHA256, new ImageChartsQrCodeProvider());
        var secret = tfa.CreateSecret(160);

        User user = new User();
        user.SetSecret(email, secret);
        user = null;

        string imgQR = tfa.GetQrCodeImageAsDataUri(email, secret);
        string imgHTML = $"<img src='{imgQR}'>";
        return imgHTML;
    }
    [HttpGet, Route("GetQRCodeAsImage")]
    public FileContentResult GetQRCodeAsImage(string email)
    {
        var tfa = new TwoFactorAuth("ManuelToscanoDEV", 6, 30, Algorithm.SHA256, new ImageChartsQrCodeProvider());
        var secret = tfa.CreateSecret(160);

        User user = new User();
        user.SetSecret(email, secret);
        user = null;

        string imgQR = tfa.GetQrCodeImageAsDataUri(email, secret);
        imgQR = imgQR.Replace("data:image/png;base64,", "");
        byte[] picture = Convert.FromBase64String(imgQR);
        return File(picture, "image/png");
    }
    [HttpGet, Route("ValidarQRCode")]
    public bool ValidarCodigo(string email, string code)
    {
        User user = new User();
        string secret = user.GetSecret(email);
        user = null;

        var tfa = new TwoFactorAuth("ManuelToscanoDEV", 6, 30, Algorithm.SHA256);
        return tfa.VerifyCode(secret, code);
    }
}