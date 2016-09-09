using System;
using System.Runtime.InteropServices;

namespace UCS.Utilities.Sodium
{
	public static class SodiumLibrary
	{
		internal delegate void _Init();

		internal delegate void _GetRandomBytes(byte[] buffer, int size);

		internal delegate int _GetRandomNumber(int upperBound);

		internal delegate void _SodiumIncrement(byte[] buffer, long length);

		internal delegate int _SodiumCompare(byte[] a, byte[] b, long length);

		internal delegate IntPtr _SodiumVersionString();

		internal delegate int _CryptoHash(byte[] buffer, byte[] message, long length);

		internal delegate int _Sha512(byte[] buffer, byte[] message, long length);

		internal delegate int _Sha256(byte[] buffer, byte[] message, long length);

		internal delegate int _GenericHash(byte[] buffer, int bufferLength, byte[] message, long messageLength, byte[] key, int keyLength);

		internal delegate int _GenericHashSaltPersonal(byte[] buffer, int bufferLength, byte[] message, long messageLength, byte[] key, int keyLength, byte[] salt, byte[] personal);

		internal delegate int _OneTimeSign(byte[] buffer, byte[] message, long messageLength, byte[] key);

		internal delegate int _OneTimeVerify(byte[] signature, byte[] message, long messageLength, byte[] key);

		internal delegate int _ArgonHashString(byte[] buffer, byte[] password, long passwordLen, long opsLimit, int memLimit);

		internal delegate int _ArgonHashVerify(byte[] buffer, byte[] password, long passLength);

		internal delegate int _ArgonHashBinary(byte[] buffer, long bufferLen, byte[] password, long passwordLen, byte[] salt, long opsLimit, int memLimit, int alg);

		internal delegate int _HashString(byte[] buffer, byte[] password, long passwordLen, long opsLimit, int memLimit);

		internal delegate int _HashBinary(byte[] buffer, long bufferLen, byte[] password, long passwordLen, byte[] salt, long opsLimit, int memLimit);

		internal delegate int _HashVerify(byte[] buffer, byte[] password, long passLength);

		internal delegate int _GenerateKeyPair(byte[] publicKey, byte[] secretKey);

		internal delegate int _GenerateKeyPairFromSeed(byte[] publicKey, byte[] secretKey, byte[] seed);

		internal delegate int _Sign(byte[] buffer, ref long bufferLength, byte[] message, long messageLength, byte[] key);

		internal delegate int _Verify(byte[] buffer, ref long bufferLength, byte[] signedMessage, long signedMessageLength, byte[] key);

		internal delegate int _SignDetached(byte[] signature, ref long signatureLength, byte[] message, long messageLength, byte[] key);

		internal delegate int _VerifyDetached(byte[] signature, byte[] message, long messageLength, byte[] key);

		internal delegate int _Ed25519SecretKeyToEd25519Seed(byte[] seed, byte[] secretKey);

		internal delegate int _Ed25519SecretKeyToEd25519PublicKey(byte[] publicKey, byte[] secretKey);

		internal delegate int _Ed25519PublicKeyToCurve25519PublicKey(byte[] curve25519Pk, byte[] ed25519Pk);

		internal delegate int _Ed25519SecretKeyToCurve25519SecretKey(byte[] curve25519Sk, byte[] ed25519Sk);

		internal delegate int _GenerateBoxKeyPair(byte[] publicKey, byte[] secretKey);

		internal delegate int _Create(byte[] buffer, byte[] message, long messageLength, byte[] nonce, byte[] publicKey, byte[] secretKey);

		internal delegate int _Open(byte[] buffer, byte[] cipherText, long cipherTextLength, byte[] nonce, byte[] publicKey, byte[] secretKey);

		internal delegate int _CreateDetached(byte[] cipher, byte[] mac, byte[] message, long messageLength, byte[] nonce, byte[] pk, byte[] sk);

		internal delegate int _OpenDetached(byte[] buffer, byte[] cipherText, byte[] mac, long cipherTextLength, byte[] nonce, byte[] pk, byte[] sk);

		internal delegate int _Bytes();

		internal delegate int _ScalarBytes();

		internal delegate byte _Primitive();

		internal delegate int _Base(byte[] q, byte[] n);

		internal delegate int _ScalarMult(byte[] q, byte[] n, byte[] p);

		internal delegate int _CreateSeal(byte[] buffer, byte[] message, long messageLength, byte[] pk);

		internal delegate int _OpenSeal(byte[] buffer, byte[] cipherText, long cipherTextLength, byte[] pk, byte[] sk);

		internal delegate int _CreateSecret(byte[] buffer, byte[] message, long messageLength, byte[] nonce, byte[] key);

		internal delegate int _OpenSecret(byte[] buffer, byte[] cipherText, long cipherTextLength, byte[] nonce, byte[] key);

		internal delegate int _CreateSecretDetached(byte[] cipher, byte[] mac, byte[] message, long messageLength, byte[] nonce, byte[] key);

		internal delegate int _OpenSecretDetached(byte[] buffer, byte[] cipherText, byte[] mac, long cipherTextLength, byte[] nonce, byte[] key);

		internal delegate int _Auth(byte[] buffer, byte[] message, long messageLength, byte[] key);

		internal delegate int _VerifyAuth(byte[] signature, byte[] message, long messageLength, byte[] key);

		internal delegate int _HmacSha256(byte[] buffer, byte[] message, long messageLength, byte[] key);

		internal delegate int _HmacSha256Verify(byte[] signature, byte[] message, long messageLength, byte[] key);

		internal delegate int _HmacSha512(byte[] signature, byte[] message, long messageLength, byte[] key);

		internal delegate int _HmacSha512Verify(byte[] signature, byte[] message, long messageLength, byte[] key);

		internal delegate int _ShortHash(byte[] buffer, byte[] message, long messageLength, byte[] key);

		internal delegate int _Encrypt(byte[] buffer, byte[] message, long messageLength, byte[] nonce, byte[] key);

		internal delegate int _EncryptChaCha20(byte[] buffer, byte[] message, long messageLength, byte[] nonce, byte[] key);

		internal delegate IntPtr _Bin2Hex(byte[] hex, int hexMaxlen, byte[] bin, int binLen);

		internal delegate int _Hex2Bin(IntPtr bin, int binMaxlen, string hex, int hexLen, string ignore, out int binLen, string hexEnd);

		internal delegate int _EncryptAead(IntPtr cipher, out long cipherLength, byte[] message, long messageLength, byte[] additionalData, long additionalDataLength, byte[] nsec, byte[] nonce, byte[] key);

		internal delegate int _DecryptAead(IntPtr message, out long messageLength, byte[] nsec, byte[] cipher, long cipherLength, byte[] additionalData, long additionalDataLength, byte[] nonce, byte[] key);

		internal delegate int _AesAvailable();

		internal delegate int _AesEncrypt(IntPtr cipher, out long cipherLength, byte[] message, long messageLength, byte[] additionalData, long additionalDataLength, byte[] nsec, byte[] nonce, byte[] key);

		internal delegate int _DecryptAes(IntPtr message, out long messageLength, byte[] nsec, byte[] cipher, long cipherLength, byte[] additionalData, long additionalDataLength, byte[] nonce, byte[] key);

		[StructLayout(LayoutKind.Sequential, Size = 384)]
		internal struct _HashState
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public ulong[] h;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public ulong[] t;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public ulong[] f;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
			public byte[] buf;

			public uint buflen;

			public byte last_node;
		}

		internal delegate int _HashInit(IntPtr state, byte[] key, int keySize, int hashSize);

		internal delegate int _HashUpdate(IntPtr state, byte[] message, long messageLength);

		internal delegate int _HashFinal(IntPtr state, byte[] buffer, int bufferLength);

		internal static LazyInvoke<SodiumLibrary._Init> _init = new LazyInvoke<SodiumLibrary._Init>("sodium_init", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._GetRandomBytes> _randombytes_buff = new LazyInvoke<SodiumLibrary._GetRandomBytes>("randombytes_buf", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._GetRandomNumber> _randombytes_uniform = new LazyInvoke<SodiumLibrary._GetRandomNumber>("randombytes_uniform", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._SodiumIncrement> _sodium_increment = new LazyInvoke<SodiumLibrary._SodiumIncrement>("sodium_increment", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._SodiumCompare> _sodium_compare = new LazyInvoke<SodiumLibrary._SodiumCompare>("sodium_compare", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._SodiumVersionString> _sodium_version_string = new LazyInvoke<SodiumLibrary._SodiumVersionString>("sodium_version_string", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._CryptoHash> _crypto_hash = new LazyInvoke<SodiumLibrary._CryptoHash>("crypto_hash", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Sha512> _crypto_hash_sha512 = new LazyInvoke<SodiumLibrary._Sha512>("crypto_hash_sha512", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Sha256> _crypto_hash_sha256 = new LazyInvoke<SodiumLibrary._Sha256>("crypto_hash_sha256", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._GenericHash> _crypto_generichash = new LazyInvoke<SodiumLibrary._GenericHash>("crypto_generichash", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._GenericHashSaltPersonal> _crypto_generichash_blake2b_salt_personal = new LazyInvoke<SodiumLibrary._GenericHashSaltPersonal>("crypto_generichash_blake2b_salt_personal", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._OneTimeSign> _crypto_onetimeauth = new LazyInvoke<SodiumLibrary._OneTimeSign>("crypto_onetimeauth", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._OneTimeVerify> _crypto_onetimeauth_verify = new LazyInvoke<SodiumLibrary._OneTimeVerify>("crypto_onetimeauth_verify", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._ArgonHashString> _crypto_pwhash_str = new LazyInvoke<SodiumLibrary._ArgonHashString>("crypto_pwhash_argon2i_str", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._ArgonHashVerify> _crypto_pwhash_str_verify = new LazyInvoke<SodiumLibrary._ArgonHashVerify>("crypto_pwhash_argon2i_str_verify", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._ArgonHashBinary> _crypto_pwhash = new LazyInvoke<SodiumLibrary._ArgonHashBinary>("crypto_pwhash_argon2i", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._HashString> _crypto_pwhash_scryptsalsa208sha256_str = new LazyInvoke<SodiumLibrary._HashString>("crypto_pwhash_scryptsalsa208sha256_str", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._HashBinary> _crypto_pwhash_scryptsalsa208sha256 = new LazyInvoke<SodiumLibrary._HashBinary>("crypto_pwhash_scryptsalsa208sha256", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._HashVerify> _crypto_pwhash_scryptsalsa208sha256_str_verify = new LazyInvoke<SodiumLibrary._HashVerify>("crypto_pwhash_scryptsalsa208sha256_str_verify", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._GenerateKeyPair> _crypto_sign_keypair = new LazyInvoke<SodiumLibrary._GenerateKeyPair>("crypto_sign_keypair", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._GenerateKeyPairFromSeed> _crypto_sign_seed_keypair = new LazyInvoke<SodiumLibrary._GenerateKeyPairFromSeed>("crypto_sign_seed_keypair", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Sign> _crypto_sign = new LazyInvoke<SodiumLibrary._Sign>("crypto_sign", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Verify> _crypto_sign_open = new LazyInvoke<SodiumLibrary._Verify>("crypto_sign_open", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._SignDetached> _crypto_sign_detached = new LazyInvoke<SodiumLibrary._SignDetached>("crypto_sign_detached", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._VerifyDetached> _crypto_sign_verify_detached = new LazyInvoke<SodiumLibrary._VerifyDetached>("crypto_sign_verify_detached", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Ed25519SecretKeyToEd25519Seed> _crypto_sign_ed25519_sk_to_seed = new LazyInvoke<SodiumLibrary._Ed25519SecretKeyToEd25519Seed>("crypto_sign_ed25519_sk_to_seed", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Ed25519SecretKeyToEd25519PublicKey> _crypto_sign_ed25519_sk_to_pk = new LazyInvoke<SodiumLibrary._Ed25519SecretKeyToEd25519PublicKey>("crypto_sign_ed25519_sk_to_pk", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Ed25519PublicKeyToCurve25519PublicKey> _crypto_sign_ed25519_pk_to_curve25519 = new LazyInvoke<SodiumLibrary._Ed25519PublicKeyToCurve25519PublicKey>("crypto_sign_ed25519_pk_to_curve25519", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Ed25519SecretKeyToCurve25519SecretKey> _crypto_sign_ed25519_sk_to_curve25519 = new LazyInvoke<SodiumLibrary._Ed25519SecretKeyToCurve25519SecretKey>("crypto_sign_ed25519_sk_to_curve25519", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._GenerateBoxKeyPair> _crypto_box_keypair = new LazyInvoke<SodiumLibrary._GenerateBoxKeyPair>("crypto_box_keypair", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Create> _crypto_box_easy = new LazyInvoke<SodiumLibrary._Create>("crypto_box_easy", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._CreateDetached> _crypto_box_detached = new LazyInvoke<SodiumLibrary._CreateDetached>("crypto_box_detached", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Open> _crypto_box_open_easy = new LazyInvoke<SodiumLibrary._Open>("crypto_box_open_easy", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._OpenDetached> _crypto_box_open_detached = new LazyInvoke<SodiumLibrary._OpenDetached>("crypto_box_open_detached", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Bytes> _crypto_scalarmult_bytes = new LazyInvoke<SodiumLibrary._Bytes>("crypto_scalarmult_bytes", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._ScalarBytes> _crypto_scalarmult_scalarbytes = new LazyInvoke<SodiumLibrary._ScalarBytes>("crypto_scalarmult_scalarbytes", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Primitive> _crypto_scalarmult_primitive = new LazyInvoke<SodiumLibrary._Primitive>("crypto_scalarmult_primitive", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Base> _crypto_scalarmult_base = new LazyInvoke<SodiumLibrary._Base>("crypto_scalarmult_base", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._ScalarMult> _crypto_scalarmult = new LazyInvoke<SodiumLibrary._ScalarMult>("crypto_scalarmult", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._CreateSeal> _crypto_box_seal = new LazyInvoke<SodiumLibrary._CreateSeal>("crypto_box_seal", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._OpenSeal> _crypto_box_seal_open = new LazyInvoke<SodiumLibrary._OpenSeal>("crypto_box_seal_open", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._CreateSecret> _crypto_secretbox = new LazyInvoke<SodiumLibrary._CreateSecret>("crypto_secretbox", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._OpenSecret> _crypto_secretbox_open = new LazyInvoke<SodiumLibrary._OpenSecret>("crypto_secretbox_open", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._CreateSecretDetached> _crypto_secretbox_detached = new LazyInvoke<SodiumLibrary._CreateSecretDetached>("crypto_secretbox_detached", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._OpenSecretDetached> _crypto_secretbox_open_detached = new LazyInvoke<SodiumLibrary._OpenSecretDetached>("crypto_secretbox_open_detached", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Auth> _crypto_auth = new LazyInvoke<SodiumLibrary._Auth>("crypto_auth", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._VerifyAuth> _crypto_auth_verify = new LazyInvoke<SodiumLibrary._VerifyAuth>("crypto_auth_verify", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._HmacSha256> _crypto_auth_hmacsha256 = new LazyInvoke<SodiumLibrary._HmacSha256>("crypto_auth_hmacsha256", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._HmacSha256Verify> _crypto_auth_hmacsha256_verify = new LazyInvoke<SodiumLibrary._HmacSha256Verify>("crypto_auth_hmacsha256_verify", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._HmacSha512> _crypto_auth_hmacsha512 = new LazyInvoke<SodiumLibrary._HmacSha512>("crypto_auth_hmacsha512", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._HmacSha512Verify> _crypto_auth_hmacsha512_verify = new LazyInvoke<SodiumLibrary._HmacSha512Verify>("crypto_auth_hmacsha512_verify", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._ShortHash> _crypto_shorthash = new LazyInvoke<SodiumLibrary._ShortHash>("crypto_shorthash", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Encrypt> _crypto_stream_xor = new LazyInvoke<SodiumLibrary._Encrypt>("crypto_stream_xor", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._EncryptChaCha20> _crypto_stream_chacha20_xor = new LazyInvoke<SodiumLibrary._EncryptChaCha20>("crypto_stream_chacha20_xor", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Bin2Hex> _sodium_bin2hex = new LazyInvoke<SodiumLibrary._Bin2Hex>("sodium_bin2hex", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._Hex2Bin> _sodium_hex2bin = new LazyInvoke<SodiumLibrary._Hex2Bin>("sodium_hex2bin", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._EncryptAead> _crypto_aead_chacha20poly1305_encrypt = new LazyInvoke<SodiumLibrary._EncryptAead>("crypto_aead_chacha20poly1305_encrypt", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._DecryptAead> _crypto_aead_chacha20poly1305_decrypt = new LazyInvoke<SodiumLibrary._DecryptAead>("crypto_aead_chacha20poly1305_decrypt", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._AesAvailable> _crypto_aead_aes256gcm_is_available = new LazyInvoke<SodiumLibrary._AesAvailable>("crypto_aead_aes256gcm_is_available", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._AesEncrypt> _crypto_aead_aes256gcm_encrypt = new LazyInvoke<SodiumLibrary._AesEncrypt>("crypto_aead_aes256gcm_encrypt", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._DecryptAes> _crypto_aead_aes256gcm_decrypt = new LazyInvoke<SodiumLibrary._DecryptAes>("crypto_aead_aes256gcm_decrypt", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._HashInit> _hash_init = new LazyInvoke<SodiumLibrary._HashInit>("crypto_generichash_init", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._HashUpdate> _hash_update = new LazyInvoke<SodiumLibrary._HashUpdate>("crypto_generichash_update", SodiumLibrary.Name);

		internal static LazyInvoke<SodiumLibrary._HashFinal> _hash_final = new LazyInvoke<SodiumLibrary._HashFinal>("crypto_generichash_final", SodiumLibrary.Name);

		internal static bool IsRunningOnMono
		{
			get
			{
				return Type.GetType("Mono.Runtime") != null;
			}
		}

		internal static bool Is64
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		internal static string Name
		{
			get
			{
				string result = SodiumLibrary.Is64 ? "libsodium-64.dll" : "libsodium.dll";
				if (SodiumLibrary.IsRunningOnMono)
				{
					result = "libsodium";
				}
				return result;
			}
		}

		internal static SodiumLibrary._Init init
		{
			get
			{
				return SodiumLibrary._init.Method;
			}
		}

		internal static SodiumLibrary._GetRandomBytes randombytes_buff
		{
			get
			{
				return SodiumLibrary._randombytes_buff.Method;
			}
		}

		internal static SodiumLibrary._GetRandomNumber randombytes_uniform
		{
			get
			{
				return SodiumLibrary._randombytes_uniform.Method;
			}
		}

		internal static SodiumLibrary._SodiumIncrement sodium_increment
		{
			get
			{
				return SodiumLibrary._sodium_increment.Method;
			}
		}

		internal static SodiumLibrary._SodiumCompare sodium_compare
		{
			get
			{
				return SodiumLibrary._sodium_compare.Method;
			}
		}

		internal static SodiumLibrary._SodiumVersionString sodium_version_string
		{
			get
			{
				return SodiumLibrary._sodium_version_string.Method;
			}
		}

		internal static SodiumLibrary._CryptoHash crypto_hash
		{
			get
			{
				return SodiumLibrary._crypto_hash.Method;
			}
		}

		internal static SodiumLibrary._Sha512 crypto_hash_sha512
		{
			get
			{
				return SodiumLibrary._crypto_hash_sha512.Method;
			}
		}

		internal static SodiumLibrary._Sha256 crypto_hash_sha256
		{
			get
			{
				return SodiumLibrary._crypto_hash_sha256.Method;
			}
		}

		internal static SodiumLibrary._GenericHash crypto_generichash
		{
			get
			{
				return SodiumLibrary._crypto_generichash.Method;
			}
		}

		internal static SodiumLibrary._GenericHashSaltPersonal crypto_generichash_blake2b_salt_personal
		{
			get
			{
				return SodiumLibrary._crypto_generichash_blake2b_salt_personal.Method;
			}
		}

		internal static SodiumLibrary._OneTimeSign crypto_onetimeauth
		{
			get
			{
				return SodiumLibrary._crypto_onetimeauth.Method;
			}
		}

		internal static SodiumLibrary._OneTimeVerify crypto_onetimeauth_verify
		{
			get
			{
				return SodiumLibrary._crypto_onetimeauth_verify.Method;
			}
		}

		internal static SodiumLibrary._ArgonHashString crypto_pwhash_str
		{
			get
			{
				return SodiumLibrary._crypto_pwhash_str.Method;
			}
		}

		internal static SodiumLibrary._ArgonHashVerify crypto_pwhash_str_verify
		{
			get
			{
				return SodiumLibrary._crypto_pwhash_str_verify.Method;
			}
		}

		internal static SodiumLibrary._ArgonHashBinary crypto_pwhash
		{
			get
			{
				return SodiumLibrary._crypto_pwhash.Method;
			}
		}

		internal static SodiumLibrary._HashString crypto_pwhash_scryptsalsa208sha256_str
		{
			get
			{
				return SodiumLibrary._crypto_pwhash_scryptsalsa208sha256_str.Method;
			}
		}

		internal static SodiumLibrary._HashBinary crypto_pwhash_scryptsalsa208sha256
		{
			get
			{
				return SodiumLibrary._crypto_pwhash_scryptsalsa208sha256.Method;
			}
		}

		internal static SodiumLibrary._HashVerify crypto_pwhash_scryptsalsa208sha256_str_verify
		{
			get
			{
				return SodiumLibrary._crypto_pwhash_scryptsalsa208sha256_str_verify.Method;
			}
		}

		internal static SodiumLibrary._GenerateKeyPair crypto_sign_keypair
		{
			get
			{
				return SodiumLibrary._crypto_sign_keypair.Method;
			}
		}

		internal static SodiumLibrary._GenerateKeyPairFromSeed crypto_sign_seed_keypair
		{
			get
			{
				return SodiumLibrary._crypto_sign_seed_keypair.Method;
			}
		}

		internal static SodiumLibrary._Sign crypto_sign
		{
			get
			{
				return SodiumLibrary._crypto_sign.Method;
			}
		}

		internal static SodiumLibrary._Verify crypto_sign_open
		{
			get
			{
				return SodiumLibrary._crypto_sign_open.Method;
			}
		}

		internal static SodiumLibrary._SignDetached crypto_sign_detached
		{
			get
			{
				return SodiumLibrary._crypto_sign_detached.Method;
			}
		}

		internal static SodiumLibrary._VerifyDetached crypto_sign_verify_detached
		{
			get
			{
				return SodiumLibrary._crypto_sign_verify_detached.Method;
			}
		}

		internal static SodiumLibrary._Ed25519SecretKeyToEd25519Seed crypto_sign_ed25519_sk_to_seed
		{
			get
			{
				return SodiumLibrary._crypto_sign_ed25519_sk_to_seed.Method;
			}
		}

		internal static SodiumLibrary._Ed25519SecretKeyToEd25519PublicKey crypto_sign_ed25519_sk_to_pk
		{
			get
			{
				return SodiumLibrary._crypto_sign_ed25519_sk_to_pk.Method;
			}
		}

		internal static SodiumLibrary._Ed25519PublicKeyToCurve25519PublicKey crypto_sign_ed25519_pk_to_curve25519
		{
			get
			{
				return SodiumLibrary._crypto_sign_ed25519_pk_to_curve25519.Method;
			}
		}

		internal static SodiumLibrary._Ed25519SecretKeyToCurve25519SecretKey crypto_sign_ed25519_sk_to_curve25519
		{
			get
			{
				return SodiumLibrary._crypto_sign_ed25519_sk_to_curve25519.Method;
			}
		}

		internal static SodiumLibrary._GenerateBoxKeyPair crypto_box_keypair
		{
			get
			{
				return SodiumLibrary._crypto_box_keypair.Method;
			}
		}

		internal static SodiumLibrary._Create crypto_box_easy
		{
			get
			{
				return SodiumLibrary._crypto_box_easy.Method;
			}
		}

		internal static SodiumLibrary._CreateDetached crypto_box_detached
		{
			get
			{
				return SodiumLibrary._crypto_box_detached.Method;
			}
		}

		internal static SodiumLibrary._Open crypto_box_open_easy
		{
			get
			{
				return SodiumLibrary._crypto_box_open_easy.Method;
			}
		}

		internal static SodiumLibrary._OpenDetached crypto_box_open_detached
		{
			get
			{
				return SodiumLibrary._crypto_box_open_detached.Method;
			}
		}

		internal static SodiumLibrary._Bytes crypto_scalarmult_bytes
		{
			get
			{
				return SodiumLibrary._crypto_scalarmult_bytes.Method;
			}
		}

		internal static SodiumLibrary._ScalarBytes crypto_scalarmult_scalarbytes
		{
			get
			{
				return SodiumLibrary._crypto_scalarmult_scalarbytes.Method;
			}
		}

		internal static SodiumLibrary._Primitive crypto_scalarmult_primitive
		{
			get
			{
				return SodiumLibrary._crypto_scalarmult_primitive.Method;
			}
		}

		internal static SodiumLibrary._Base crypto_scalarmult_base
		{
			get
			{
				return SodiumLibrary._crypto_scalarmult_base.Method;
			}
		}

		internal static SodiumLibrary._ScalarMult crypto_scalarmult
		{
			get
			{
				return SodiumLibrary._crypto_scalarmult.Method;
			}
		}

		internal static SodiumLibrary._CreateSeal crypto_box_seal
		{
			get
			{
				return SodiumLibrary._crypto_box_seal.Method;
			}
		}

		internal static SodiumLibrary._OpenSeal crypto_box_seal_open
		{
			get
			{
				return SodiumLibrary._crypto_box_seal_open.Method;
			}
		}

		internal static SodiumLibrary._CreateSecret crypto_secretbox
		{
			get
			{
				return SodiumLibrary._crypto_secretbox.Method;
			}
		}

		internal static SodiumLibrary._OpenSecret crypto_secretbox_open
		{
			get
			{
				return SodiumLibrary._crypto_secretbox_open.Method;
			}
		}

		internal static SodiumLibrary._CreateSecretDetached crypto_secretbox_detached
		{
			get
			{
				return SodiumLibrary._crypto_secretbox_detached.Method;
			}
		}

		internal static SodiumLibrary._OpenSecretDetached crypto_secretbox_open_detached
		{
			get
			{
				return SodiumLibrary._crypto_secretbox_open_detached.Method;
			}
		}

		internal static SodiumLibrary._Auth crypto_auth
		{
			get
			{
				return SodiumLibrary._crypto_auth.Method;
			}
		}

		internal static SodiumLibrary._VerifyAuth crypto_auth_verify
		{
			get
			{
				return SodiumLibrary._crypto_auth_verify.Method;
			}
		}

		internal static SodiumLibrary._HmacSha256 crypto_auth_hmacsha256
		{
			get
			{
				return SodiumLibrary._crypto_auth_hmacsha256.Method;
			}
		}

		internal static SodiumLibrary._HmacSha256Verify crypto_auth_hmacsha256_verify
		{
			get
			{
				return SodiumLibrary._crypto_auth_hmacsha256_verify.Method;
			}
		}

		internal static SodiumLibrary._HmacSha512 crypto_auth_hmacsha512
		{
			get
			{
				return SodiumLibrary._crypto_auth_hmacsha512.Method;
			}
		}

		internal static SodiumLibrary._HmacSha512Verify crypto_auth_hmacsha512_verify
		{
			get
			{
				return SodiumLibrary._crypto_auth_hmacsha512_verify.Method;
			}
		}

		internal static SodiumLibrary._ShortHash crypto_shorthash
		{
			get
			{
				return SodiumLibrary._crypto_shorthash.Method;
			}
		}

		internal static SodiumLibrary._Encrypt crypto_stream_xor
		{
			get
			{
				return SodiumLibrary._crypto_stream_xor.Method;
			}
		}

		internal static SodiumLibrary._EncryptChaCha20 crypto_stream_chacha20_xor
		{
			get
			{
				return SodiumLibrary._crypto_stream_chacha20_xor.Method;
			}
		}

		internal static SodiumLibrary._Bin2Hex sodium_bin2hex
		{
			get
			{
				return SodiumLibrary._sodium_bin2hex.Method;
			}
		}

		internal static SodiumLibrary._Hex2Bin sodium_hex2bin
		{
			get
			{
				return SodiumLibrary._sodium_hex2bin.Method;
			}
		}

		internal static SodiumLibrary._EncryptAead crypto_aead_chacha20poly1305_encrypt
		{
			get
			{
				return SodiumLibrary._crypto_aead_chacha20poly1305_encrypt.Method;
			}
		}

		internal static SodiumLibrary._DecryptAead crypto_aead_chacha20poly1305_decrypt
		{
			get
			{
				return SodiumLibrary._crypto_aead_chacha20poly1305_decrypt.Method;
			}
		}

		internal static SodiumLibrary._AesAvailable crypto_aead_aes256gcm_is_available
		{
			get
			{
				return SodiumLibrary._crypto_aead_aes256gcm_is_available.Method;
			}
		}

		internal static SodiumLibrary._AesEncrypt crypto_aead_aes256gcm_encrypt
		{
			get
			{
				return SodiumLibrary._crypto_aead_aes256gcm_encrypt.Method;
			}
		}

		internal static SodiumLibrary._DecryptAes crypto_aead_aes256gcm_decrypt
		{
			get
			{
				return SodiumLibrary._crypto_aead_aes256gcm_decrypt.Method;
			}
		}

		internal static SodiumLibrary._HashInit hash_init
		{
			get
			{
				return SodiumLibrary._hash_init.Method;
			}
		}

		internal static SodiumLibrary._HashUpdate hash_update
		{
			get
			{
				return SodiumLibrary._hash_update.Method;
			}
		}

		internal static SodiumLibrary._HashFinal hash_final
		{
			get
			{
				return SodiumLibrary._hash_final.Method;
			}
		}
	}
}
