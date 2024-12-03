//CRUD
const database = require('../config/database');
const mysql2 = require('mysql2');

const readuser = (req,res)=>{
    const { id } = req.params;

    const readquery = `SELECT * FROM usuario WHERE id_usuario=?`;

    const query = mysql2.format(readquery,[id]);

    database.query(query, (err,result) => {
        if (err) throw err;
        if(result[0] !== undefined){
            res.json(result[0]);
        }else{
            res.json({message: 'Usuario no encontrado'});
        }
    });
};

const readscore = (req, res) => {
    const { id } = req.params;

    const readquery = `SELECT puntaje, nhabitaciones, horas, minutos, segundos, enemigosderro FROM puntuaciones WHERE idusuario = ?`;
    const query = mysql2.format(readquery, [id]);

    database.query(query, (err, result) => {
        if (err) {
            return res.status(500).json({ success: false, message: 'Error en el servidor.' });
        }
        if (result.length > 0) {
            res.status(200).json({ success: true, puntuaciones: result });
        } else {
            res.status(404).json({ success: false, message: 'No se encontraron puntuaciones para este usuario.' });
        }
    });
};

const createuser = (req,res)=>{
    const {nombre, password,anonacimiento,mesnacimiento,dianacimiento,edad,correo} = req.body;
    
    const checkEmailQuery = `SELECT * FROM usuario WHERE correo = ?`;
    const checkEmail = mysql2.format(checkEmailQuery, [correo]);
    database.query(checkEmail, (err, result) => 
    {
        if (err) throw err;

        if (result.length > 0) {
            // Si el correo ya existe, enviar un mensaje de error
            res.json({ success: false, message: 'El correo ya está registrado.' });
        } else {
            const createQuery = `INSERT INTO usuario(nombre,password,anonacimiento,mesnacimiento,dianacimiento,edad,correo) VALUE(?,?,?,?,?,?,?)`;

            const query = mysql2.format(createQuery, [nombre, password,anonacimiento,mesnacimiento,dianacimiento,edad,correo]);

            database.query(query, (err,result) => {
                if (err) throw err;
                console.log(result);
                const userId = result.insertId;
                    database.query('SELECT * FROM usuario WHERE id_usuario = ?', [userId], (error, results) => {
                        if (err) throw err; 
                        else {
                            res.status(200).json({ success: true, userData: results[0] });
                        }
                })
            })
        }});
    };


const updateuser = (req,res)=>{
    const {id}  = req.params;
    const {nombre, edad} = req.body;
    
    const updateQuery = `UPDATE usuario SET nombre=?,edad=? WHERE id_usuario=?`

    const query = mysql2.format(updateQuery, [nombre,edad,id]);

    database.query(query, (err,result) => {
        if (err) throw err;
        console.log(result);
        res.send({message: 'Usuario actualizado'});
    });
};

const deleteuser = (req,res)=>{
    const {id}  = req.params;
    
    const deleteQuery = `DELETE FROM usuario WHERE id_usuario=?`;

    const query=mysql2.format(deleteQuery,[id]);

    database.query(query, (err,result) => {
        if (err) throw err;
        console.log(result);
        res.send({message: 'Usuario eliminado'});
    });
};



const loginUser = (req, res) => {
    const { correo, password } = req.body;

    const loginQuery = `SELECT * FROM usuario WHERE correo=? AND password=?`;
    const query = mysql2.format(loginQuery, [correo, password]);

    database.query(query, (err, result) => {
        if (err) throw err;
        if (result[0] !== undefined) {
            res.json({ success: true, userData: result[0] });
        } else {
            res.json({ success: false, message: 'Usuario no encontrado' });
        }
    });
};

const guardarPuntuacion = (req, res) => {
    const { puntaje, nhabitaciones, horas, minutos, segundos, enemigosderro, idusuario } = req.body;

    // Verificar que se recibió idusuario
    if (!idusuario) {
        return res.status(400).json({ success: false, message: 'ID de usuario no proporcionado.' });
    }

    const guardarQuery = `
        INSERT INTO puntuaciones (puntaje, nhabitaciones, horas, minutos, segundos, enemigosderro, idusuario)
        VALUES (?, ?, ?, ?, ?, ?, ?)
    `;
    const query = mysql2.format(guardarQuery, [puntaje, nhabitaciones, horas, minutos, segundos, enemigosderro, idusuario]);

    database.query(query, (err, result) => {
        if (err) throw err;
        console.log(result);
        res.status(200).json({ success: true, message: 'Puntuación guardada exitosamente.' });
    });
};

const getGlobalScores = (req, res) => {
    const query = `
        SELECT 
            p.id_estadistica, 
            p.puntaje, 
            p.nhabitaciones, 
            p.horas, 
            p.minutos, 
            p.segundos, 
            p.enemigosderro, 
            u.nombre
        FROM 
            puntuaciones p
        INNER JOIN 
            usuario u 
        ON 
            p.idusuario = u.id_usuario
        ORDER BY 
            p.puntaje DESC
    `;

    database.query(query, (err, results) => {
        if (err) {
            console.error('Error al ejecutar la consulta:', err);
            return res.status(500).json({ error: 'Error al obtener los datos.' });
        }

        if (results && results.length > 0) {
            // Devolvemos los resultados
            res.status(200).json({ success: true, data: results });
        } else {
            // No se encontraron resultados
            res.status(404).json({ success: false, message: 'No se encontraron puntuaciones.' });
        }
    });
};



module.exports = {
    getGlobalScores,
    createuser,
    readuser,
    updateuser,
    deleteuser,
    loginUser,  // Asegúrate de exportar la nueva función
    guardarPuntuacion,
    readscore,
};