export class intervention {
  date: string;
  description: string;
  spentHours: number;
  completed: boolean;
  constructor(date: string, spentHours: number, description: string, completed: boolean) {
    this.date = date
    this.description = description
    this.spentHours = spentHours
    this.completed = completed
  }
}
