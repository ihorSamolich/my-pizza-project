export interface IUser {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  photo: string;
}

export interface ILogin {
  email: string;
  password: string;
}

export interface ILoginResponse {
  token: string;
}
