# Unity Certain Camera Recorder

### Installation:
1. Open [releases] (https://github.com/mentoster/Unity-CertainCameraScreenshot/releases)
2. Download the RecorderScript.unitypackage package when unity is open.
3. Allow import in the unity editor. (If you don't need a camera controller, disable Ghost script import)

### How use:
1. Drag the video or screenshot camera's prefab to the scene.
2. If  u need to change the buttons in the controllers, u can do it.
3. Launch the scene and click the appropriate button. (C-for screenshot, V-for video, by default)

## Wow! U awesome! Now about the rest
* if the camera doesn't work, it is disabled to improve performance
* The video resolution depends on the texture, so if you want another resolution, just change the texture resolution.
* Screenshots automatically adjusts to your screen.
* The program Automatically creates a folder in your project called Screenshots and throws screenshots there or video!
* You can also change  a  recording name and the path to save screenshots (videos).

### Mistakes that should be ignored:
1. [monsters from Doom] (https://yadi.sk/i/GJBnVsEFowxMrQ)
2. Sooooo, here's what this beast tells us:
* ReadPixels was called to read pixels from system frame buffer, while not inside drawing frame.
* UnityEngine.Texture2D:ReadPixels(Rect, Int32, Int32)
* ScreenShot:LateUpdate()
3. Translate from unity language : you used a method not intended for making screenshots (that's the news, right?)!
* This arises from the fact that I use LateUpdate()
* And the screenshots are working through ScrnCam.Render();
* If you use the method for screenshots, the screenshot will be taken from the main camera, it can be bad, if u want to take screenshot from another camera.
4. I haven't figured out how to tell unity to stop displaying the error yet, so I suggest you put up with it for now.

### Установка:
1. Открыть [релизы](https://github.com/mentoster/Unity-CertainCameraScreenshot/releases)
2. Скачать пакет RecorderScript.unitypackage при открытом unity.
3. Разрешить импорт в редакторе unity. (В случае, если вам не нужен контроллер камеры - отключите импорт Ghost skript)

### Использование:
1.  Перетащить  префаб необходимой  камеры на сцену, с которой необходимо сделать скриншот или видео.
2.  В случае необходимости поменять кнопки в контроллерах.
3. Запустить сцену и нажать необходимую кнопку. (С-для скриншота, V-для видео, по умолчанию)

## Ура! Теперь остальное!
  * Если видео не записывается, или не делается скриншот - камеры не работают, для повышения производительности соответсвенно.
  * Разрешение видео зависит от текстуры, поэтому если вы хотите другое разрешение, просто поменяйте разрешение текстуры.
  * Скриншоты Автоматически  подстраивается под ваш экран.
  * Программа Автоматически  создаёт папку в вашем проекте под названием Screenshots и кидает туда скриншоты или видео в похожую папку!
  * Вы так же можете изменить формат записи имени и путь сохранения скриншотов(видео).
  
### Про видео:
1. Видео записывается благодаря декодированию каждого кадра текстуры в картинку.
* Время работы приложения замедляется, для того, чтобы каждый отрендеренный кадр был сохранен в папку.
2. Картинка записывается в папку игры.
3. Новое  видео - новая папка с картинками. 
4. Пока Необходимо воспользоваться сторонней программой, для того, чтобы собрать из картинки  видео.
5. Звука нет.

### Ошибки на которые стоит наплевать:
1. [Ваши кровные враги](https://yadi.sk/i/GJBnVsEFowxMrQ)
2. Таааак, вот что говорит нам эта зверюга:
* ReadPixels was called to read pixels from system frame buffer, while not inside drawing frame.
* UnityEngine.Texture2D:ReadPixels(Rect, Int32, Int32)
* ScreenShot:LateUpdate() 
3. Перевожу с unityвского - вы воспользововались методом, не предназначенным для делания скриншотов (вот так новость, правда?)!
* Это возникает из-за того что я использую LateUpdate()
* И скриншоты  работают благодаря ScrnCam.Render();
* Если использовать метод для скриншотов, то скриншот будет браться с основной камеры (больно надо).
4. Я пока не разобрался, как сказать unity перестать отображать ошибку, поэтому пока предлагаю вам с этим смириться.  

