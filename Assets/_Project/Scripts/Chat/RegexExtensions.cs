using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class RegexExtensions
{
	// Regex : Regular Expression (정규 표현식)

	// 닉네임으로 0~9, a~z, A~Z, 가~힣 안에 포함되는 완성형 한글과 영문/숫자만 포함하는 정규식
	// @ : 문자열 그대로 출력. 역슬래시 기능 안됨, 큰따옴표는 두개 해야 하나 나온다.
	// ^ : 역비트 연산자
	private static Regex nicknameRegex = new Regex(@"[^0-9A-Za-z가-힣]");

	// Regex 사용하는 방법
	// 1. Regex 클래스 사용
	// this : 확장 메서드 사용을 위해 선언
	public static bool NicknameValidate(this string nickname)
	{
		return false == nicknameRegex.IsMatch(nickname);
	}

	// 2. Regex 포맷 문자열로 변환
	// 문자열 입력 중에 미완성형 한글을 허용하는 정규식
	const string INPUT_FORM = @"[^0-9A-Za-z가-힣ㄱ-ㅎㅏ-ㅣㆍᆞᆢ]";

	public static string ToValidString(this string param)
	{
		return Regex.Replace(param, INPUT_FORM, "", RegexOptions.Singleline);
	}
}