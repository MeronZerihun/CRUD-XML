using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Xml;

using Notes.Models;

namespace Notes.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index() 
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("../notes.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("notes");
            XmlNodeList notes = RootNode.ChildNodes; 

            

            return View(notes);

        }
        public ActionResult Create(Note note)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(Server.MapPath("../notes.xml"));
            XmlNode RootNode = XmlDoc.SelectSingleNode("notes");

           
            XmlNode noteNode = RootNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "note", ""));

            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "title", "")).InnerText = note.title;
            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "body", "")).InnerText = note.body;
            
            XmlDoc.Save(Server.MapPath("../notes.xml"));


            return RedirectToAction("Index");

        }
        public ActionResult Edit(String id) 
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(Server.MapPath("../notes.xml"));
            XmlNode RootNode = XmlDoc.SelectSingleNode("notes");
            XmlNodeList notes = RootNode.ChildNodes;

            XmlNode note = notes[0];

            foreach (XmlNode n in notes) 
            {
                if (n["id"].InnerText == id)
                {
                     note = n;
                }
            }

            return View(note);

        }
        public ActionResult Update(Note n) 
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("../notes.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("notes");
            XmlNodeList notes = RootNode.ChildNodes;

            XmlNode note = notes[0]; 

            foreach (XmlNode ns in notes)
            {
                if (ns["id"].InnerText == n.id.ToString())
                {
                    note = ns;
                }
            }

            note["title"].InnerText = n.title;
            note["body"].InnerText = n.body;
            
            XmlDocObj.Save(Server.MapPath("../notes.xml"));

            return RedirectToAction("Index");

        }

        public ActionResult Delete(String id)
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("../notes.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("notes");
            XmlNodeList notes = RootNode.ChildNodes;

            XmlNode note = notes[0]; 

            foreach (XmlNode n in notes)
            {
                if (n["id"].InnerText == id)
                {
                    note = n;
                }
            }

            RootNode.RemoveChild(note);

            XmlDocObj.Save(Server.MapPath("../notes.xml"));

            return RedirectToAction("Index");
        }
    }
}
