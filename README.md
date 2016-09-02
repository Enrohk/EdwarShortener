#Autoevaluación

En este apartado voy a realizar la autoevaluación de cada una de las partes del proyecto.

#Usuarios

##Suficiente
- El usuario podrá registrarse y hacer logIn

##Notable
- Además el login incluye mecanismos de seguridad

##Sobresaliente
- Existe un pequeño cliente de HTML que ofrece añadir y modificar unas cuantas opciones típicas de perfil.


Se ha realizado  hasta el nivel sobresaliente.
Para llegar al cliente HTMl hay que hacer logIn en la aplicación, la cual utiliza un sistema de autenticación para no tener que introducir el nombre de usuario y contraseña cada vez que se haga una petición. Si no hay un usuario que haya iniciado sesión se auto-inicia sesión con el usuario global, para que las estadísticas de usuarios no registrados no se pierdan.
Para mantener la sesión iniciada se utiliza el framework de nancy Authentication.Forms el cual provee una serie de interfaces para implementar que se encargan de hacer el LogIN LogOut y Register, así como mantener el GUID del usuario y su nombre de usuario (y los Claims que se crean necesarios) en las peticiones al servidor.

Además se ha añadido un pequeño sistema de cifrado de contraseñas para la base de datos basado en el algoritmo MD5.


#Mostrar estadísticas

##Suficiente
- De cada enlace acortado se almacenará el número de veces que se ha utilizado el enlace, la fecha de creación y la URL de destino
- Se podrá negociar que el contenido sea un HTML o un JSON. SI se pide un HTML se devolverá otra URL donde habrá una página de HTML con dicha información. Si se pide JSON se devolverá la información.

##Notable
- Existe un servicio que para un patrón de URI determinado devuelve información agregada por países, unidades administrativas o por ciudades del los clientes durante un periodo determinado (todo, desde, hasta, desde-hasta) en formato JSON
- La información anterior se muestra en formato de tabla o gráfico

##Sobresaliente
- Al servicio anterior se puede filtrar por un una caja que delimita área y el contenido devuelto tiene asociado coordenadas de latitud y longitud
- La información anterior se muestra en un cliente de mapas

Por el momento se ha llegado al nivel suficiente, guardando el número de accesos y el momento en que se realiza el acceso.
Hay una pequeña base para el proceso del notable ya que si que se puede buscar con un desde-hasta el número de clicks de una URL acortada en concreto, tanto para un usuario registrado como para el usuario global del sistema.
Tambíén están preparados los mapas utilizado en framework de google google.charts, asi como una serie de vistas (análogas a las que mostraba goo.gl antes de la última actualización), pero no esta implementada la funcionalidad, solo la definición de los graficos.

#Url segura y alcanzable

##Suficiente
- Solo se puede crear una URI acortada si se verifica que una petición HTTP GET síncrona a la URI original devuelve una respuesta con estado 200

##Notable
- Nivel de servicio suficiente pero la comprobación es asíncrona (creación no bloqueante)
- Se verifica periódicamente la respuesta que la URI original devuelve.
- Si la última respuesta de verificación periódica no ha devuelto 200 o no es alcanzable, se devolverá una página HTML con un error 404 indicando desde cuándo dicha URI no es alcanzable. Este mensaje estará activo hasta que la verificación periódica vuelva a devolver 200.

##Sobresaliente
- Se verifica periódicamente la respuesta que la URI original devuelve un contenido correcto verificado mediante una serie de reglas
- Si la última respuesta de verificación periódica ha no ha devuelto 200 o no es alcanzable, se devolverá la página cacheada
- El creador de la URI podrá, al crearla o una vez que se autentique (no importa el sistema), podrá añadir, borrar o modificar una serie de reglas (que tendrá cada una su URI) que aplicada a esa URI determinará el contenido es correcto; las reglas están expresadas en un lenguaje script que se ejecutará en el servidor

En esta funcionalidad se ha llegado al suficiente y se ha dejado preparado el notable, pero no se ha conseguido.
Se ha preparado de la siguiente forma, a la BD se ha añadido un campo de pageStatus que indica el último estado de la página, así como otro campo de tipo DateTime que indica la última vez que se perdió el estado de OK de la página.
La primera ejecución sigue siendo síncrona con la petición, para realizar la petición asíncrona y temporal para verificar el estado de cada una de las URL acortadas, se creó un WindowsService que se ejecutaba cada día a las 5 de la mañana, pero primero por falta de información y de conocimientos de los servicios de windows por mi parte y por problemas con internet no he logrado que el servicio funcione. Por lo que la funcionalidad de Notable NO está realizada.

#CSV

##Suficiente
- El usuario enviará un fichero CSV con las URI a recortar y  el servidor, aplicando todas las comprobaciones vigentes, recortará todas o ninguna (si en alguna falla), devolviendo el resultado en un JSON

##Notable
- Nivel de servicio suficiente pero no bloqueante; para ello se devolverá una URI que representa el trabajo de conversión y consultado indicará el estado del proceso y la URI donde obtener el resultado cuando esté disponible
- El servidor podrá descartar las URI no válidas sin interrumpir el proceso.

##Sobresaliente
- Un cliente HTML permitirá copiar en un formulario todas las URI que se deseen, y  a continuación, enviará de forma continua todas las URI al servidor y recibirá y mostrará de forma asíncrona la información de las URI acortadas.

En esta funcionalidad tambien se ha llegado solo al nivel suficiente de forma completa.
La parte de cliente del sobresaliente esta hecha pero no he sido capaz de realizar las llamadas asincronas, me daban problemas al recibir los datos y no he sabido porque,
nuevamente una falta de conocimientos de las tecnologias (aunque me ha gustado mucho trabajar con ellas) no me ha permitido realizar lo propuesto.
Digamos que estoy en el nivel sobresaliente pero la llamada es bloqueante. Lo cual como servicio web no tiene sentido.

#Conclusiones

Como evaluación general tengo que decir que el proyecto no es ni de lejos lo que tendría que haber entregado, prácticamente no hay ninguna funcionalidad que sea digna y lo que hay es bastante muy justo.

No puedo poner una excusa válida excepto mi propio mala gestión del tiempo y exceso de confianza, empecé el proyecto en junio y deje una base (que yo creía bastante decente, y a la hora de la verdad no lo era). Durante julio dejé el proyecto abandonado por motivos de trabajo, pero a la hora de retomarlo en Agosto lo hice tarde y ya no me dio tiempo ha realizar algo mejor que creo tengo la capacidad de hacer.  debido a los viajes al pueblo donde no tengo internet (cosa que debería haber previsto y no hice) no pude trabajar lo que tendría que haber trabajado en Agosto. Como digo la única razón es una falta de organización por mi parte.

Respecto a las tecnologías:

Se han utilizado las siguientes tecnologías:

NancyFx para el servidor de aplicaciones REST
.Net coon Razor View para la parte del cliente
Framework de Nancy.Authentication.Forms
SQLServer para la base de datos
EntityFramework para la gestión del repositorio de la base de datos.
Windows Services para hacer un servicio (fallido)
Bootstrap para mejorar el look&feel
Mi evaluación de las tecnologías es más positiva que la general del proyecto, con las tecnologías sí que estoy contento, he aprendido bastante (aun falta mucho por aprender) y he encontrado algo nuevo para programar que no conocía demasiado, quiza tambien este a sido un poco el problema de no haber gestionado bien el tiempo. En concreto me ha gustado mucho como se acoplan EntityFramework con Linq y la facilidad para hacer un CRUD legible y sencillo para SQL.

#Despliegue

No he podido probar el despliegue en otro ordenador, pero teóricamente debería ser así:
instalar VisualStudio 2015
instalar una versión SQL Server
ejecutar en SQL Server el script de creación de la base de datos que se encuentra en el proyecto.
añadir el proyecto de este mismo GitHub
Ejecutar en visual studio
Como digo no lo he probado, asi que no estoy 100% seguro de que esto funcione.

Respecto a tener algo en la nube, he intentado sin éxito subirlo a Azure con mi cuenta de la universidad. Tenía un plan B por si esto me fallaba que consistía en hacer un pequeño cambio en la aplicación (cambiar el módulo de las rutas, por un Main que haga lo mismo) y desplegarlo en el Docker de un compañero de la universidad. Pero por motivos personales no he podido venir a zaragoza hasta  ayer noche, por lo que me ha sido imposible hacerlo (de nuevo falta de previsión)

