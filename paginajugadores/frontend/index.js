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


console.log('Holiwis!');