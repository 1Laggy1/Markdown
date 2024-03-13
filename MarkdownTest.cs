using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit;
namespace Markdown
{
    [TestFixture]
    internal class MarkdownTest
    {
        [Test]
        public void TestAdd()
        {
            string result = Program.ConvertMarkdownToHtml("Це текст у форматі Markdown. В межах нашої роботи ми розглядаємо дуже урізану підмножину цієї мови розмітки. Текст можна розділяти на абзаци, лишаючи між абзацами 1 (один) порожній рядок.\r\n\r\nМи можемо виділяти текст **жирним**, робити його _курсивним_ або `фіксованої ширини`. Також є можливість зберігати фрагменти преформатованого тексту:\r\n```\r\nЦей текст є преформатований. \r\n   Це означає, що він відобразиться, як є, і жодне інше форматування, як от **жирний** чи _курсив_ на нього не \r\nвпливає.          А ще тут усі пробіли, відступи та перенесення рядка зберігаються як є\r\n```\r\n\r\n_ - а це нижнє підкреслення\r\n**_** - теж ок\r\nsnake_case\r\n_italic case_\r\n");
            Assert.That(result, Is.EqualTo("\n<p>Це текст уtest2 форматі Markdown. В межах нашої роботи ми розглядаємо дуже урізану підмножину цієї мови розмітки. Текст можна розділяти на абзаци, лишаючи між абзацами 1 (один) порожній рядок.</p>\n<p>Ми можемо виділяти текст <b>жирним</b>, робити його <i>курсивним</i> або <tt>фіксованої ширини</tt>. Також є можливість зберігати фрагменти преформатованого тексту:\r\n<pre>\r\nЦей текст є преформатований. \r\n   Це означає, що він відобразиться, як є, і жодне інше форматування, як от <b>жирний</b> чи <i>курсив</i> на нього не \r\nвпливає.          А ще тут усі пробіли, відступи та перенесення рядка зберігаються як є\r\n</pre></p>\n<p>_ - а це нижнє підкреслення\r\n<b>_</b> - теж ок\r\nsnake_case\r\n<i>italic case</i>\r\n</p>"));
            string errorMessage = "";
            try
            {
                result = Program.ConvertMarkdownToHtml("<b><i><tt>");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Assert.That(errorMessage, Is.EqualTo("Wrong format: " + "<b><i><tt>"));
        }
    }
}
