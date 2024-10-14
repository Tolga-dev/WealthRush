// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("XsRlkry0rfb1vqEGP9qafbxky+afZAixnGaAdVxgDqV3WpcKDtgh9JMQHhEhkxAbE5MQEBGqPkmqBUkRQd5HG7XWE2XVJqK78cECollfXlIhkxAzIRwXGDuXWZfmHBAQEBQREt8MpdLgJmFzQ+qbIeTQWM1a91/xjQBZiuoav37A47I5axegqhBXJTPTPF+aa0hLYzuwGkPGtbxj0MmSpMjt5DFAPSnh9IxRO+577bg2bqhhwlU2hCuJfhdKOOXF4qMRphfefMetqb9h14dgixFLhOBsF1zEpRmUpVbSWZ1+EU/l8lZdBv9utZjEhBNZzUanDwoNd9L9O/djCF+zRbMDa9yuu9oLNM1ccZwWjBPmiZUd7z6S76HshFuATJOtyBMSEBEQ");
        private static int[] order = new int[] { 3,12,12,7,7,5,11,13,11,12,12,12,13,13,14 };
        private static int key = 17;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
