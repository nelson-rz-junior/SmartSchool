import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Teacher } from '../models/Teacher';

@Injectable({
  providedIn: 'root'
})
export class TeacherService
{
  baseURL = `${environment.mainUrlApi}/professor`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Teacher[]> {
    return this.http.get<Teacher[]>(this.baseURL);
  }

  getById(id: number): Observable<Teacher> {
    return this.http.get<Teacher>(`${this.baseURL}/${id}`);
  }

  getByDisciplineId(id: number): Observable<Teacher[]> {
    return this.http.get<Teacher[]>(`${this.baseURL}/disciplina/${id}`);
  }

  getByStudentId(id: number): Observable<Teacher[]> {
    return this.http.get<Teacher[]>(`${this.baseURL}/aluno/${id}`);
  }

  post(teacher: Teacher) {
    return this.http.post(this.baseURL, teacher);
  }

  put(teacher: Teacher) {
    return this.http.put(`${this.baseURL}/${teacher.id}`, teacher);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
