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

const createuser = (req,res)=>{
    const {nombre, password,anonacimiento,mesnacimiento,dianacimiento,edad,correo} = req.body;

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
            });
    });

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

module.exports = {
    createuser,
    readuser,
    updateuser,
    deleteuser,
    loginUser,  // Asegúrate de exportar la nueva función
};