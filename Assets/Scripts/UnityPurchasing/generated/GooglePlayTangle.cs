// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("0478G9R6vhj1ETuOEazphVHEwkGpy9Bq6HIldzMV2pnAicfYwM8025KqOgxwPNVJ8ZOYutmXiJDMCsUPrw9XEbw8QBOFdva3LfsCQcScbN/EsONFGowOk5FngyhPSLd81Y7KRXHy/PPDcfL58XHy8vNX4IrMQ8ZoTaEvK6ZGR78QeRAA/5XpFsmlSqQfvhiOe9APi0eDx6IsIN2xxPLhjcNx8tHD/vX62XW7dQT+8vLy9vPwHCMUsjaxZs+lMLZRh7j00azZt6aJMBe2fr/e3qj9PdcSqhTe0oO2LQG26+GNHt86GwYpfyOKyuGtHrYZUWuoCB9Lhj67sjBIdhNuuVchcrqVbFnygKyzIILqJODymbCeidQ6n0Afg7A0iw0wevHw8vPy");
        private static int[] order = new int[] { 8,12,6,7,9,12,6,11,8,10,13,11,13,13,14 };
        private static int key = 243;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
