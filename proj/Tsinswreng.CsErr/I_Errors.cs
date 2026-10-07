namespace Tsinswreng.CsErr;

[Doc($@"Container for error collection")]
public partial interface I_Errors{
	[Doc($@"List of errors, can be string, Exception, etc")]
	public IList<obj?> Errors{get;set;}
}

[Doc($@"錯誤視圖之適配: 把單個錯誤項轉成{nameof(ITypedErrView)}")]
public static class ErrView{
	[Doc($@"
	試圖把單個錯誤項適配成{nameof(ITypedErrView)}、不能適配則返 null。
	{nameof(ITypedErrView)} 原樣返;
	{nameof(IErrNode)} 以其全路徑爲 {nameof(ITypedErrView.Key)}、其標籤爲 {nameof(ITypedErrView.Tags)};
	其餘一概不能適配。
	")]
	public static ITypedErrView? AsOrToErrView(obj? Err){
		// 本來就是視圖的 原樣返、無需再造
		if(Err is ITypedErrView View){
			return View;
		}
		// 錯誤鍵: 正是視圖之 Key 所指的那類東西
		// 它本身沒有 Args、其全路徑即爲鍵
		if(Err is IErrNode Key){
			return new TypedErrView{
				Key = Key.ToString(),
				// 複製一份標籤、不與錯誤鍵共用集合,
				// 以免日後改動視圖時連帶改到 KeysErr 裏那些靜態錯誤鍵
				Tags = new HashSet<str>(
					Key.Tags??new HashSet<str>()
				),
			};
		}
		// 其餘一概不能適配。
		// Key 是錯誤之標識(鍵樹上的一條路徑、用來查 i18n 模板),
		// 字符串則是一條現成的人話、裸 Exception 是未經分類的故障,
		// 兩者都沒有標識可言、在這套「鍵 + 模板 + 參數」的視圖體系裏沒有位置。
		// 把原文塞進 Key 或 Args 硬湊出一個視圖, 只是讓洞看起來被填上了。
		return null;
	}
}

public static class I_ErrorsExtn{
	[Doc($@"Adds an error to {nameof(I_Errors.Errors)} and returns self for fluent chaining")]
	public static TSelf AddErr<TSelf>(
		this TSelf z, obj Err
	)where TSelf : class, I_Errors
	{
		z.Errors ??= new List<object?>();
		z.Errors.Add(Err);
		return z;
	}

	[Doc($"""
	把錯誤列表中所有能適配成{nameof(ITypedErrView)}的
	都適配 然後組成扁平的 一唯的 錯誤視圖列表。
	不能適配成異常視圖的就被忽略

	同一個容器在同一條遞歸路徑上只進入一次, 以免錯誤容器互相引用時無限遞迴。
	防止
	var e = new TypedErr();
	e.Errors.Add("數據庫連線失敗");
	var views = e.ExtractErrViews();   // views.Count == 0
	""")]
	public static IList<ITypedErrView> ExtractErrViews(this I_Errors z){
		var R = new List<ITypedErrView>();
		// 只記錄當前這條遞歸路徑上已進入的容器, 進入時加入、返回時移除。
		// 用引用比較: 實現類可能重寫 Equals, 那會把內容相同但不同的兩個容器當成同一個。
		var OnPath = new HashSet<obj>(ReferenceEqualityComparer.Instance);
		ExtractErrViewsInto(z, R, OnPath);
		return R;
	}

	[Doc($@"
	{nameof(ExtractErrViews)} 的遞歸主體: 逐個處理 z.Errors 中的項。
	第三個參數記錄當前這條遞歸路徑上已進入的容器, 用來擋環路。
	")]
	static void ExtractErrViewsInto(
		I_Errors z, IList<ITypedErrView> R, ISet<obj> OnPath
	){
		// 本容器已在本條路徑上, 再展開就會成環, 故直接返回
		if(!OnPath.Add(z)){
			return;
		}
		foreach(var err in z.Errors){
			// 能適配成視圖的 收下(錯誤鍵由此得以進入視圖列表)
			if(ErrView.AsOrToErrView(err) is ITypedErrView View){
				R.Add(View);
			}
			// 容器型錯誤 遞歸攤平其內層錯誤。
			// 容器自身亦可能是視圖(如 TypedErr)、那時上面與此處會各處理一次, 此爲刻意
			if(err is I_Errors Errs){
				ExtractErrViewsInto(Errs, R, OnPath);
			}
		}
		// 離開本容器時把它移出, 這樣它出現在另一條不相干的分支時仍會照常展開
		OnPath.Remove(z);
	}

	
	[Doc(@$"
	若z無內容則拋異常
	取首元素 試圖轉換爲 {nameof(ITypedErrView)}
	若失敗亦拋。
	若成功 則使首元素之Type作返值之類型
	z.Errors中之餘者即照加入返值之Errors
	")]
	public static TypedErr ToTypedErr(this I_Errors z, OptParseView Opt){
		// 1: 無內容即拋
		if(z.Errors is null || z.Errors.Count == 0){
			throw new Exception(@$"{nameof(z)}.{nameof(I_Errors.Errors)} is empty");
		}
		// 2: 首元素轉爲錯誤視圖、失敗即拋
		var First = ErrView.AsOrToErrView(z.Errors[0]);
		if(First is null){
			throw new Exception(@$"{nameof(z)}.{nameof(I_Errors.Errors)}[0] cannot be converted to {nameof(ITypedErrView)}");
		}
		// 3: 首元素之Type作返值之類型 —— FromView 正是幹這個的
		var R = TypedErr.FromView(First, Opt);
		// 4: 餘者原樣加入、不轉換、不攤平
		for(var i = 1; i < z.Errors.Count; i++){
			R.AddErr(z.Errors[i]!);
		}
		return R;
	}
}
