#import "@preview/tsinswreng-auto-heading:0.1.0": auto-heading
#let H = auto-heading;

#H[概述][
	CsErr 是一個錯誤處理庫，提供類型安全的錯誤處理機制。

	#H[設計理念][
		- *IAnswer*：替代 `try-catch` 處理業務錯誤，用返回值包裝結果
		- *AppErr*：應用異常類，支持錯誤分類與 I18n 模板
		- *錯誤標籤*：區分業務異常/系統異常、公開/私有
	]

	#H[何時使用][
		- *IAnswer*：業務邏輯中的預期錯誤（如參數校驗失敗、資源不存在）
		- *throw*：預料外的系統異常（如數據庫連接失敗、空指針）
	]
]

#H[帶參數錯誤與 I18n][
	#H[模板機制][
		錯誤 Key 使用 `__` 作為參數佔位符，`Args` 存儲參數值。調用 `ToString()` 時自動填充：

		```
		Key:  "User__NotFound"
		Args: ["userId123"]
		結果: "User[userId123]NotFound"
		```

		多參數示例：
		```
		Key:  "ParseErrorAtFile__Line__Col__"
		Args: ["MyFile", 0, 1]
		結果: "ParseErrorAtFile[MyFile]Line[0]Col[1]"
		```
	]

	#H[I18n 集成][
		`Key` 可作為國際化查找鍵，根據語言環境返回對應翻譯：

		```cs
		// 定義錯誤項（Key 路徑）
		var errItem = ErrItem.MkB(null, new List<str>{"User", "__", "NotFound"});

		// 創建帶參數錯誤
		var err = errItem.ToErr("user123");

		// 前端根據 Key 查找翻譯
		// zh-Hant: "用戶 user123 不存在"
		// en: "User user123 not found"
		```
	]
]

#H[使用示例][
	#H[IAnswer 替代 try-catch][
		```cs
		// 傳統方式
		public User GetUser(int id){
			var user = _repo.Find(id);
			if(user == null) throw new Exception("User not found");
			return user;
		}

		// IAnswer 方式
		public IAnswer<User> GetUser(int id){
			var result = new Answer<User>();
			var user = _repo.Find(id);
			if(user == null){
				return result.AddErr("User not found");
			}
			return result.OkWith(user);
		}

		// 調用方
		var result = GetUser(123);
		if(result.Ok){
			var user = result.Data;
		}else{
			var errors = result.ErrsToStrs();
		}
		```
	]

	#H[定義錯誤類型][
		```cs
		// 定義錯誤項樹
		static class Errors{
			public static IErrItem Root = ErrItem.Mk(null, new List<str>{"App"});

			public static IErrItem User = ErrItem.MkB(Root, new List<str>{"User"});
			public static IErrItem UserNotFound = ErrItem.MkB(User, new List<str>{"__", "NotFound"});
			public static IErrItem UserInvalidEmail = ErrItem.MkB(User, new List<str>{"InvalidEmail"});
		}
		```
	]

	#H[創建帶參數錯誤][
		```cs
		// 方式一：通過 ErrItem
		var err = Errors.UserNotFound.ToErr("user123");
		// Key: "App.User.__.NotFound", Args: ["user123"]

		// 方式二：通過 AppErr.Mk
		var err = AppErr.Mk(Errors.UserNotFound, "user123");

		// 獲取錯誤字符串
		var msg = err.ToString(); // "App.User[user123].NotFound"
		```
	]

	#H[錯誤嵌套][
		```cs
		var result = new Answer<Order>();
		var orderErr = Errors.OrderNotFound.ToErr("order123");
		var userErr = Errors.UserNotFound.ToErr("user456");

		result.AddErr(orderErr).AddErr(userErr);

		// 扁平化獲取所有錯誤視圖
		var views = result.ToErrViews();
		```
	]

	#H[Web API 響應][
		```cs
		[HttpGet("user/{id}")]
		public IWebAns<User> GetUser(int id){
			var result = GetUserInternal(id);
			if(!result.Ok){
				return WebAns.Mk(null, result.ToErrViews());
			}
			return WebAns.Mk(result.Data);
		}

		// 響應格式
		// { "data": {...}, "errors": [{"key": "App.User.__.NotFound", "args": ["user123"], "tags": [...]}] }
		```
	]

	#H[錯誤標籤判斷][
		```cs
		var err = Errors.UserNotFound.ToErr("user123");

		if(err.Tags.Contains(ErrTags.BizErr)){
			// 業務異常，可顯示給用戶
		}

		if(err.Tags.Contains(ErrTags.SysErr)){
			// 系統異常，記錄日誌
		}

		if(err.Tags.Contains(ErrTags.Public)){
			// 可公開給用戶
		}
		```
	]

	#H[調試參數][
		```cs
		// DebugArgs 不會傳給前端，僅用於服務端日誌
		var err = Errors.DbError.ToErr("connection timeout")
			.AddDebugArgs("server=db.example.com", "port=5432");
		```
	]
]

#H[錯誤標籤說明][
	| 標籤 | 說明 |
	|------|------|
	| `BizErr` | 業務異常，如參數不合法、資源不存在 |
	| `SysErr` | 系統異常，如數據庫錯誤、網絡超時 |
	| `Public` | 可顯示給終端用戶 |
	| `Private` | 不應顯示給用戶（含敏感信息） |
]
