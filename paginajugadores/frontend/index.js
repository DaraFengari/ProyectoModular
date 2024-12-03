const formGet= document.getElementById('getUsuario');
const formPost = document.getElementById('postUsuario');
const formPut= document.getElementById('putUsuario');
const formDelete= document.getElementById('deleteUsuario');

if(formGet){
    formGet.addEventListener('submit',async(e)=>{
        e.preventDefault();
        let message ='';
        const id_usuario= e.target.id.value;
        
        await fetch(`http://127.0.0.1:3000/user/${id_usuario}`).then((response)=>response.json()).then((data)=>{
            if(data.message){
                message = data.message;
            }else{
                message = `ID: ${data.id_usuario} | Nombre: ${data.nombre} | edad ${data.edad}`;
            };
    
            console.log(data);
        });
    
        document.getElementById('textoGet').innerHTML = message;
    });
}

if (formPost) {
    formPost.addEventListener('submit', async (e) => {
        e.preventDefault();
        let message = '';
        const nombre = e.target.nombre.value;
        const edad = e.target.Edad.value;
        const mes = e.target.mesnacimiento.value;
        const year = e.target.anonacimiento.value;
        const dia = e.target.dianacimiento.value;
        const pass = e.target.password.value;
        const mail = e.target.correo.value;

        // Validación del correo
        const emailPattern = /^[a-z0-9._%+-]+@(gmail|hotmail|outlook)+\.[a-z]{2,}$/;
        if (!emailPattern.test(mail)) {
            alert('Por favor, introduce un correo válido.');
            return;
        }

        // Validación de la contraseña
        if (pass.length < 8) {
            alert('La contraseña debe tener al menos 8 caracteres.');
            return;
        }

        // Validación de caracteres especiales, números, y letras en la contraseña
        const passwordPattern = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
        if (!passwordPattern.test(pass)) {
            alert('La contraseña debe contener al menos una letra, un número y un carácter especial.');
            return;
        }

        // Validación de nombre
        if (nombre.length < 2) {
            alert('El nombre debe tener al menos 2 caracteres.');
            return;
        }

        await fetch('http://127.0.0.1:3000/user/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                nombre: nombre,
                password: pass,
                edad: edad,
                anonacimiento: year,
                mesnacimiento: mes,
                dianacimiento: dia,
                correo: mail
            })
        }).then((response) => response.json())
        .then((data) => {
            if (data.success) { // Verifica si el registro fue exitoso
                localStorage.setItem('userData', JSON.stringify(data.userData));
                window.location.href = '/frontend/views/dashboard.html';  // Redirige al dashboard
            } else {
                message = data.message;
            }
        });

        //document.getElementById('textoPost').innerHTML = message;
    });
}



if(formPut){
    formPut.addEventListener('submit',async(e)=>{
        e.preventDefault();
        let message = '';
        const id_usuario=e.target.id.value;
        const nombre= e.target.nombre.value;
        const edad = e.target.Edad.value;
    
        console.log('funcion iniciada');
        await fetch(`http://127.0.0.1:3000/user/${id_usuario}`,{
            method: 'PUT',
            headers:{
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({nombre: nombre, edad:edad}),
        })
            .then((response)=>response.json())
            .then((data)=>{
            message = data.message;
        });
        
        document.getElementById('textoPut').innerHTML = message;
    });
}

if(formDelete){
    formDelete.addEventListener('submit',async(e)=>{
        e.preventDefault();
        const id_usuario=e.target.id.value;
        let message='';
        await fetch(`http://127.0.0.1:3000/user/${id_usuario}`,{
            method:'DELETE'
        })
        .then((response)=>response.json()).then((data)=>{
            if(data.message){
                message = data.message;
            }else{
                message = `ID: ${data.id_usuario} | Nombre: ${data.nombre} | edad ${data.edad}`;
            };
    
            console.log(data);
        });
    
        document.getElementById('textoDelete').innerHTML = message;
    
    });
}

const loginForm = document.getElementById('loginForm');

if (loginForm) {
    loginForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        const correo = e.target.email.value;
        const password = e.target.password.value;

        const emailPattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/;
        if (!emailPattern.test(correo)) {
            alert('Por favor, introduce un correo válido.');
            return;
        }

        // Validación de la contraseña
        if (password.length < 8) {
            alert('La contraseña debe tener al menos 8 caracteres.');
            return;
        }

        let loginMessage = '';

        await fetch('http://127.0.0.1:3000/user/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ correo, password }),
        })
            .then((response) => response.json())
            .then((data) => {
                if (data.success) {
                    // Guardar datos del usuario en el localStorage o enviarlos al dashboard
                    localStorage.setItem('userData', JSON.stringify(data.userData));
                    window.location.href = '/frontend/views/dashboard.html';  // Redirige al dashboard
                } else {
                    loginMessage = data.message;
                }
            });

        document.getElementById('loginMessage').innerHTML = loginMessage;
    });
}

const formGlobalScores = document.getElementById('globalScores');

if (formGlobalScores) {
    formGlobalScores.addEventListener('submit', async (e) => {
        e.preventDefault();
        let message = '';

        await fetch('http://127.0.0.1:3000/user/escoreglobal')
            .then((response) => response.json())
            .then((data) => {
                if (data.length > 0) {
                    // Si hay resultados, crear una tabla con las puntuaciones
                    let table = `
                        <table>
                            <thead>
                                <tr>
                                    <th>Puntaje</th>
                                    <th>Habitaciones</th>
                                    <th>Horas</th>
                                    <th>Minutos</th>
                                    <th>Segundos</th>
                                    <th>Enemigos Derrotados</th>
                                    <th>Nombre del Usuario</th>
                                </tr>
                            </thead>
                            <tbody>
                    `;
                    data.forEach((item) => {
                        table += `
                            <tr>
                                <td>${item.puntaje}</td>
                                <td>${item.nhabitaciones}</td>
                                <td>${item.horas}</td>
                                <td>${item.minutos}</td>
                                <td>${item.segundos}</td>
                                <td>${item.enemigosderro}</td>
                                <td>${item.nombre}</td>
                            </tr>
                        `;
                    });
                    table += `</tbody></table>`;
                    message = table;
                } else {
                    message = 'No hay puntuaciones disponibles.';
                }
            })
            .catch((error) => {
                console.error('Error:', error);
                message = 'Ocurrió un error al obtener las puntuaciones.';
            });

        document.getElementById('globalScoresContainer').innerHTML = message;
    });
}

// index.js



console.log('Holiwis!');