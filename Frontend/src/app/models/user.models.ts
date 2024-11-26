export enum Role {
  Customer,
  Administrator,
}

export interface User {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    role: Role;
  }
  
  export interface CreateUserDto {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    role: Role;
  }
  
  export interface UpdateUserDto {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    role: Role;
  }
  