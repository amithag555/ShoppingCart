using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Data
{
    public class ProductContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Title = "בוגאטי",
                    ShortDescription = "בוגאטי ויירון EB 16.4",
                    LongDescription = @"בוגאטי ויירון (Bugatti Veyron) היא מכונית-על קיצונית מהמהירות בעולם, אשר יוצרה על ידי חברת בוגאטי שבבעלות תאגיד המכוניות האירופי פולקסווגן בין השנים 2006-2015. שמו המלא של הדגם הוא Veyron EB 16.4. הוויירון מונעת במנוע בתצורת W16 בעל נפח שמונה ל' וארבעה מגדשי טורבו. היא מאיצה מ-0 ל-96 קמ""ש ב-2.46 שניות ומגיעה למהירות מרבית של 408.48 קמ""ש, למרות משקלה הכבד יחסית של 1,888 קילוגרם (משקל כולל נוזלים עומד על כ-2,300 קילוגרם).",
                    Price = 2000000,
                    ImageMimeType1 = "image/jpeg",
                    ImageName1 = "car2.jpg"
                },
                new Product
                {
                    ProductId = 2,
                    Title = "אינפרנו",
                    ShortDescription = "אינפרנו",
                    LongDescription = @"אינפרנו הינה מכונית על קונספטואלית המגיעה ממקסיקו ומיוצרת באיטליה. מכונית זו מציגה מבנה העשוי מתשלובת מתכות מיוחדת הכוללת כסף טהור, אלומיניום ואבץ. בכל הנוגע לפרטים הטכניים לאינפרנו יש לא פחות מ- 1400 כ""ס המגיעים ממנוע בנזין כפול מגדשי טורבו בתצורת V8 אשר מאיצים את המכונית למאה קמ""ש ב- 3 שניות ועד למהירות מקסימלית של 395 קמ""ש.",
                    Price = 3000000,
                    ImageMimeType1 = "image/jpeg",
                    ImageName1 = "car3.jpg"
                },
                new Product
                {
                    ProductId = 3,
                    Title = "אאודי",
                    ShortDescription = "אאודי TTS",
                    LongDescription = @"ה-TTS, כמו כל S מבית אאודי, היא הגרסה החמה של הדגם, ה-S מציעה יותר סוסים, הנעה כפולה ויותר אבזור. ל-TTS, יש אותו מנוע 2.0 ליטר טורבו בו פגשנו כבר בעבר ב-TT. דגם זה מגיע עם 286 כ""ס ואלו מושגים בטווח שבין 5600 ל-6200 סל""ד. התאוצה ל-100 קמ""ש עומדת על 4.7 שניות (0.1 שניות פחות מאשר זו עם האורווה המלאה, 0.7 שניות מהר מזו שנמצאת מתחתיה). ה-TTS מוצעת בארץ עם תיבה דו-מצמדית. ",
                    Price = 800000,
                    ImageMimeType1 = "image/jpeg",
                    ImageName1 = "car4.jpg"

                },
                new Product
                {
                    ProductId = 4,
                    Title = "טסלה",
                    ShortDescription = "טסלה רודסטר",
                    LongDescription = @"הרודסטר של טסלה מבוססת על שלדה של מכונית הספורט הבריטית לוטוס אליס, אך במקום מנוע הבנזין, לבה של הטסלה הוא מנוע חשמלי המפיק 248 כ""ס ומוזן על ידי סוללת ליתיום איון. על פי נתוני היצרנית, היא מזנקת מ-0 ל-100 קמ""ש בפחות מארבע שניות, ומהירותה המרבית היא 201 קמ""ש.",
                    Price = 2500000,
                    ImageMimeType1 = "image/jpeg",
                    ImageName1 = "car5.jpg"
                },
                new Product
                {
                    ProductId = 5,
                    Title = "פגאני",
                    ShortDescription = "פגאני הואיירה BC",
                    LongDescription = @"Pagani Huayra Roadster BC היא מכונית העל שמגלמת את הפיתוח הטכני וההנדסי האולטימטיבי של פגאני עם המחקר הכי מצוין לקבלת סגנון ואלגנטיות. זינגה בנתה מחדש בקפדנות את המכונית ב-CSR2, בדיוק כפי שפגאני מייצרת בעבודת יד את המכונית האמיתית עבור הלקוחות שלה. ",
                    Price = 4000000,
                    ImageMimeType1 = "image/jpeg",
                    ImageName1 = "car1.jpg"
                },
                new Product
                {
                    ProductId = 6,
                    Title = "מרצדס",
                    ShortDescription = "מרצדס CLA",
                    LongDescription = @"מרצדס CLA החדשה מהווה את הגרסה הספורטיבית יותר למשפחת דגמי ה-A קלאס. מציגה חידושים רבים למערכת MBUX, כולל זיהוי מחוות ידיים, מענה לשאילתות מורכבות וזיהוי קולי אישי. דגם ה- CLA250 מצויד במנוע 2.0 ליטר טורבו עם 225 כ""ס ו-35.5 קג""מ המשודך לתיבת 7 הילוכים כפולת מצמדים. ",
                    Price = 1500000,
                    ImageMimeType1 = "image/jpeg",
                    ImageName1 = "car6.jpg"
                });
        }
    }
}
