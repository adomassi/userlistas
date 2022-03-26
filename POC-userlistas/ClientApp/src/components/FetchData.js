import React, { Component } from 'react';
import UploadService from '../components/Upload-fileservice';
import http from "../components/http-common.js";

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);   

      this.selectFile = this.selectFile.bind(this);
      this.upload = this.upload.bind(this);

      this.state = {
          selectedFiles: undefined, 
          users: [], loading: true,
          newUserName: "",
          fileInfos: [],
          userIdToEdit: 0,
          nameToEdit: "",
      };
  }

  componentDidMount() {
      this.populateUsers();    
    }

    async upload() {
        let currentFile = this.state.selectedFiles[0];
        const result = await UploadService.upload(currentFile);
        console.log(result);
        this.setState({
            selectedFiles: undefined           
        });

        this.populateUsers();
    }

    selectFile(event) {      
        this.setState({
            selectedFiles: event.target.files,
        });
    }

    setName(event) {       
        this.setState({ newUserName: event.target.value });
    }

    setModifyName(event) {
        this.setState({ nameToEdit: event.target.value });
    }


    async modifyUser() {
        const result = await http.put("users", JSON.stringify({
            'name': this.state.nameToEdit,
            'id': this.state.userIdToEdit}));
        if (result.data.success) {
            this.populateUsers();
        }
        else {
            console.log('error: ' + result.data.error);
        }
    }

    async addUser() {            
        const result = await http.post("users", JSON.stringify({ 'name': this.state.newUserName }));
        if (result.data.success) {
            this.populateUsers();
        }
        else {
            console.log('error: ' + result.data.error);
        }
    }

    async deleteUser(id) {
        const result = await http.delete("users/" + id);
        if (result.data.success) {
            this.populateUsers();
        }
        else {
            console.log('error: ' + result.data.error);
        }
    }

    editUser(user, cancel) {
        if (!!cancel) {
            this.setState({ userIdToEdit: 0 })
        }
        else {
            this.setState({ userIdToEdit: user.id, nameToEdit: user.name })
        }        
    }

    static renderUsersTable(state, editUser, setModifyName, modifyUser, deleteUser) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>                       
                    </tr>
                </thead>
                <tbody>
                    {state.users.map(user =>
                        <tr key={user.id}>
                            {state.userIdToEdit == user.id ?
                                (
                                <React.Fragment>
                                        <input type="text" value={state.nameToEdit} onChange={setModifyName.bind(this)} />
                                <button
                                    className="btn"
                                            onClick={modifyUser.bind(this)}
                                >
                                    save
                                </button>
                                <button
                                className="btn"
                                onClick={() => editUser(user, true)}
                                >
                                Cancel
                                </button></React.Fragment>)
                                
                                :
                                <React.Fragment>
                                <td>{user.name}</td>
                                <button
                                    className="btn"
                                        onClick={() => editUser(user)}
                                >
                                Edit
                                </button>
                                <button
                                    className="btn"
                                        onClick={() => deleteUser(user.id)}
                                >
                                    Delete
                                </button>
                                </React.Fragment>
                            }                           
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

      render() {
          let contents = this.state.loading
              ? <p><em>Loading...</em></p>
              : FetchData.renderUsersTable(this.state, this.editUser.bind(this),
                  this.setModifyName.bind(this), this.modifyUser.bind(this),
                  this.deleteUser.bind(this));

          return (
              <div>
                  <h1 id="tabelLabel" >userlistas :)</h1>
                  <p>This component demonstrates fetching/editing/deleting data from the server.</p>
                  {contents}

                  <label className="btn btn-default">
                      <input type="file" onChange={this.selectFile} />
                  </label>

                  <button
                      className="btn btn-success"
                     
                      onClick={this.upload}
                  >
                      Upload
                  </button>                           
                  <div>
                  <label className="btn btn-default">
                        <input type="text" value={this.state.newUserName } onChange={this.setName.bind(this)} />
                      </label>
                      <button
                          className="btn btn-success"

                          onClick={this.addUser.bind(this)}
                      >
                          add user
                      </button>
                </div>

              </div>
          );
      }

    async populateUsers() {
        const users = await http.get("users");       
        if (users.data.success) {
            this.setState({ users: users.data.resultObject, loading: false, newUserName: "", nameToEdit: "", userIdToEdit: 0 });
        }
        else {
            console.log('error: ' + users.data.error);
        }
    }; 
  }

