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
            XmlDocObj.Load(Server.MapPath("~/notes.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("notes");
            XmlNodeList notes = RootNode.ChildNodes;
            List<Note> notesList = new List<Note>();
            foreach(XmlNode node in notes)
            {
                if(node!=null)
                    notesList.Add(new Note { body = node.SelectSingleNode("body").InnerText, id = node.SelectSingleNode("id").InnerText, title = node.SelectSingleNode("title").InnerText});
            }
            return View(notesList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Note note)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(Server.MapPath("~/notes.xml"));
            XmlNode RootNode = XmlDoc.SelectSingleNode("notes");
            XmlNodeList notesList = RootNode.ChildNodes;

            XmlNode noteNode = RootNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "note", ""));

            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "id", "")).InnerText = new Random().Next(0,int.MaxValue).ToString();
            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "title", "")).InnerText = note.title;
            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "body", "")).InnerText = note.body;
            
            XmlDoc.Save(Server.MapPath("~/notes.xml"));


            return RedirectToAction("Index");

        }
        public ActionResult Edit(String id) 
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(Server.MapPath("~/notes.xml"));
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
            Note note_object = new Note { body = note.SelectSingleNode("body").InnerText, id = note.SelectSingleNode("id").InnerText, title = note.SelectSingleNode("title").InnerText };
            return View(note_object);

        }
        public ActionResult Update(Note n) 
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("~/notes.xml"));
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
            
            XmlDocObj.Save(Server.MapPath("~/notes.xml"));

            return RedirectToAction("Index");

        }

        public ActionResult Delete(String id)
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("~/notes.xml"));
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

            note.RemoveAll();
            RootNode.RemoveChild(note);

            XmlDocObj.Save(Server.MapPath("~/notes.xml"));

            return RedirectToAction("Index");
        }
    }
}
