using AjaxExample.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjaxExample.Controllers
{
    public class BookController : Controller
    {

        private BookAjaxEntities _context;

        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        public BookController()
        {
            _context = new BookAjaxEntities();
        }

        /// <summary>
        /// mặc định khi gọi đến BookController trang Index sẽ được chạy đầu tiên
        /// và sẽ gọi đến phương thức GetBooks()
        /// </summary>
        /// <returns>books</returns>
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// trả về một danh sách những record
        /// </summary>
        /// <returns>books</returns>
        public ActionResult GetBooks()
        {
            var books = _context.Books.ToList();

            return Json(books, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// trả về record tương ứng với tham số id đã truyền vào
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>book</returns>
        public ActionResult Get(int id)
        {
            var book = _context.Books.ToList().Find(m => m.Id == id);
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// tạo mới một record.
        /// Bind Exclude = "Id" ở đây có nghĩa là khi ta thêm mới một record
        /// thì mặc định phương thức Create sẽ bỏ qua giá trị của Id vì ở đây Id chúng ta
        /// đã thiết lập tự động tăng
        /// </summary>
        /// <param name="book">book</param>
        /// <returns>book</returns>
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }

            return Json(book, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// chỉnh sửa thông tin của một record
        /// </summary>
        /// <param name="book">book</param>
        /// <returns>book</returns>
        [HttpPost]
        public ActionResult Update(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(book).State = EntityState.Modified;
                _context.SaveChanges();
            }

            return Json(book, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// xóa record tương ứng với tham số id đã truyền vào
        /// </summary>
        /// <param name="id"></param>
        /// <returns>book</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var book = _context.Books.ToList().Find(m => m.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }

            return Json(book, JsonRequestBehavior.AllowGet);
        }
    }
}