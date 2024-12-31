using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace moodyfox.Controllers
{

    public class ServiceController : Controller
    {
        // GET: Service

        [Route("services/2DAnimation")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("services/3DAnimation")]
        public ActionResult _3DAnimation()
        {
            return View();
        }

        [Route("services/3DProductVideos")]
        public ActionResult _3DProductVideos()
        {
            return View();
        }

        [Route("services/ExplainerVideos")]
        public ActionResult _ExplainerVideos()
        {
            return View();
        }

        [Route("services/MotionGraphics")]
        public ActionResult _MotionGraphics()
        {
            return View();
        }

        [Route("services/eLearningVideos")]
        public ActionResult _eLearningVideos()
        {
            return View();
        }
    }
}