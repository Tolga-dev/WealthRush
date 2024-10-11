// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("mC9yeBSHRqOCn7DmuhNTeDSHL4BdKXrcgxWXCgj+GrHW0S7lTBdT3MjyMZGG0h+nIiup0e+K9yDOuOsj6Gtlalroa2Bo6Gtras55E1XaX/E2ls6IJaXZihzvby60YpvYXQX1RhCpji/nJkdHMWSkToszjUdLGi+0Shdlgk3jJ4FsiKIXiDVwHMhdW9jUOLayP9/eJongiZlmDHCPUDzTPTBSSfNx67zuqoxDAFkQXkFZVq1CCzOjlemlTNBoCgEjQA4RCVWTXJaFuo0rryj/VjypL8geIW1INUAuPwz1wGsZNSq5G3O9eWsAKQcQTaMGWuhrSFpnbGNA7CLsnWdra2tvammGJ4EX4kmWEt4aXju1uUQoXWt4FNmGGimtEpSp42hpa2pr");
        private static int[] order = new int[] { 3,9,4,9,7,13,8,7,12,12,10,13,12,13,14 };
        private static int key = 106;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
