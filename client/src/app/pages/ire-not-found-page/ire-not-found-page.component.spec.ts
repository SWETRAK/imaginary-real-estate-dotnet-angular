import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreNotFoundPageComponent } from './ire-not-found-page.component';

describe('IreNotFoundPageComponent', () => {
  let component: IreNotFoundPageComponent;
  let fixture: ComponentFixture<IreNotFoundPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreNotFoundPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreNotFoundPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
